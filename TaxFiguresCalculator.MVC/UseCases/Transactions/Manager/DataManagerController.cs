using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Helpers;
using TaxFiguresCalculator.Core.Repositories;
using TaxFiguresCalculator.Core.Services;
using TaxFiguresCalculator.MVC.Interfaces;
using TaxFiguresCalculator.MVC.ViewModels;

namespace TaxFiguresCalculator.MVC.Controllers
{
    public class DataManagerController : Controller
    {
        private readonly IDataManagerViewService _dataManagerViewService;
        private readonly IAccountRepository _accountRepository;
        private readonly IAsyncRepository<Transaction> _asyncTransactionRepository;
        private readonly IAsyncRepository<Customer> _asyncCustomerRepository;
        private readonly ITransactionRepository _transactionRepository;
        

        /// <summary>
        /// Data Manager Controller.
        /// Handles Display/Editing/Deleting of Client Transaction Related Data.
        /// </summary>
        /// <param name="dataManagerViewService"></param>
        /// <param name="accountRepository"></param>
        /// <param name="asyncTransactionRepository"></param>
        /// <param name="customerAsyncRepository"></param>
        public DataManagerController(IDataManagerViewService dataManagerViewService,
            IAccountRepository accountRepository,
            IAsyncRepository<Transaction> asyncTransactionRepository,
            IAsyncRepository<Customer> asyncCustomerRepository, 
            ITransactionRepository transactionRepository)
        {
            _dataManagerViewService = dataManagerViewService;
            _accountRepository = accountRepository;
            _asyncTransactionRepository = asyncTransactionRepository;
            _asyncCustomerRepository = asyncCustomerRepository;
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="customerId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(IFormCollection collection, int? customerId, int? page)
        {
            //Hard Coded Value for Transactions Displayed Per Page, could be configurable.
            var transactionsPerPage = 10;


            //Better more efficient way for passing data can be achieved. 
            //Through Session Variables or better forwarding of variables between requests
            if (collection["CustomerIdForData"].ToString() != "")
            {
                customerId = customerId ?? Convert.ToInt32(collection["CustomerIdForData"]);
            }

            var customers = await _asyncCustomerRepository.ListAllAsync();

            if (!customers.Any(x => x.Id == customerId))
            {
                return RedirectToAction("Index", "TransactionsPortal");
            }

            var TransactionViewModel = await _dataManagerViewService.GetTransactionsByCustomerAsync(customerId ?? 0, page ?? 0, transactionsPerPage);
            
            return View("Index", TransactionViewModel);
        }

        /// <summary>
        /// Retrieves To Be Edited Transaction Model View Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var transactionViewModel = _dataManagerViewService.GetTransactionViewModel(id);
            return View("Edit", transactionViewModel);
        }

        /// <summary>
        /// Retrieves To Be Edited Transaction Model View Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var transactionViewModel = _dataManagerViewService.GetTransactionViewModel(id);
            return View("Delete", transactionViewModel);
        }

        /// <summary>
        /// Post Method for persisting Transaction Entity Updates.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(IFormCollection collection)
        {
            int CustomerId = Convert.ToInt32(collection["CustomerId"]);
           
            ///Checks for Model State 
            if (!ModelState.IsValid)
            {
                RedirectToAction("Index", new { customerId = CustomerId });
            }

            //Validates Model Data, can be achieved through IValidator DataAnnotations or Tag Helpers
            //Validation Messages can be added to assist the user
            ValueObjectsHelper valueObjectsHelper = new ValueObjectsHelper();
            if (valueObjectsHelper.TryGetCurrencySymbol(collection["Currency"], out string symbol) ||
            valueObjectsHelper.TryGetValidDecimal(collection["Amount"]))
            {

                /// Updates Transaction Entity on Db. Can be decoupled and injected to Service
                var updatedTransaction = await _transactionRepository.GetByIdCompositeAsync(Convert.ToInt32(collection["TransactionId"]), collection["accountName"]);
                updatedTransaction.Description = collection["Description"].ToString();
                updatedTransaction.Amount = Decimal.Parse(collection["Amount"], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
                updatedTransaction.CurrencyCode = collection["Currency"];

                await _asyncTransactionRepository.UpdateAsync(updatedTransaction);
            }

            var transactionViewModel = _dataManagerViewService.GetTransactionViewModel(Convert.ToInt32(collection["TransactionId"]));

            return View("Edit", transactionViewModel);
        }

        /// <summary>
        /// Post Method For Persisting Transaction Entity Deletion
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <param name="accountId"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(int? TransactionId, string accountId, int CustomerId)
        {
            //Check if Model is Valid
            if (!ModelState.IsValid)
            {
                RedirectToAction("Index", new { customerId = CustomerId });
            }

            var toBeDeleteTransaction = await _transactionRepository.GetByIdCompositeAsync((long)TransactionId, accountId);

            await _asyncTransactionRepository.DeleteAsync(toBeDeleteTransaction);

            return RedirectToAction("Index", new { customerId = CustomerId });
        }
    }
}
