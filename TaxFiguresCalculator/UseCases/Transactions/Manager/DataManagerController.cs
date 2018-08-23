using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Repositories;
using TaxFiguresCalculator.Core.Services;
using TaxFiguresCalculator.MVC.Interfaces;
using TaxFiguresCalculator.MVC.ViewModels;

namespace TaxFiguresCalculator.MVC.Controllers
{
    public class DataManagerController : Controller
    {
        private readonly IDataManagerViewService _dataManagerViewService;
        private readonly IAccountRepository _accountrepository;
        private readonly IRepository<Transaction> _iTransactionrepository;
        private readonly IAsyncRepository<Customer> _customerrepository;


        public DataManagerController(IDataManagerViewService dataManagerViewService,
            IAccountRepository transactionrepository,
            IRepository<Transaction> iTransactionrepository,
            IAsyncRepository<Customer> customerrepository)
        {
            _dataManagerViewService = dataManagerViewService;
            _accountrepository = transactionrepository;
            _iTransactionrepository = iTransactionrepository;
            _customerrepository = customerrepository;

        }
        public async Task<IActionResult> Index(IFormCollection collection,int? customerId, int?page)
        {
            var itemsPage = 10;

             if (collection["CustomerIdForData"].ToString() != "")
            {
                
                 customerId = customerId ?? Convert.ToInt32(collection["CustomerIdForData"]);
            }
            var customers = await _customerrepository.ListAllAsync();
            if (!customers.Any(x => x.Id == customerId)){
                return RedirectToAction("Index", "Customer");
            }
            var TransactionViewModel = await _dataManagerViewService.GetTransactionsByCustomerAsync(customerId ?? 0, page ?? 0, itemsPage);
            return View("Index", TransactionViewModel);
        }
        public ActionResult Edit(int id)
        {
            var transactionViewModel = _dataManagerViewService.GetTransactionViewModel(id);
            return View("Edit", transactionViewModel);
        }
        public ActionResult Delete(int id)
        {
            var transactionViewModel = _dataManagerViewService.GetTransactionViewModel(id);
            return View("Delete", transactionViewModel);
        }

        [HttpPost]
        public ActionResult Edit(IFormCollection collection)
        {
            int CustomerId = Convert.ToInt32(collection["CustomerId"]);
            if (!ModelState.IsValid)
            {
                RedirectToAction("Index", new { customerId = CustomerId });
            }
            var updatedTransaction = _iTransactionrepository.GetByIdComposite(Convert.ToInt32(collection["TransactionId"]),collection["accountName"]);
            updatedTransaction.Description = collection["Description"].ToString();
            updatedTransaction.Amount =Decimal.Parse(collection["Amount"],System.Globalization.NumberStyles.AllowDecimalPoint,System.Globalization.CultureInfo.InvariantCulture);
            updatedTransaction.CurrencyCode = collection["Currency"];
            _iTransactionrepository.Update(updatedTransaction);
            
            var transactionViewModel = _dataManagerViewService.GetTransactionViewModel(Convert.ToInt32(collection["TransactionId"]));
           

            return View("Edit", transactionViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int? TransactionId, string accountId,int CustomerId)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Index", new { customerId = CustomerId });
            }
            var toBeDeleteTransaction = _iTransactionrepository.GetByIdComposite((long)TransactionId, accountId);
            _iTransactionrepository.Delete(toBeDeleteTransaction);
           return RedirectToAction("Index", new { customerId = CustomerId });
        }
    }
}
