using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Repositories;
using TaxFiguresCalculator.Infrastracture.DataAccess;
using TaxFiguresCalculator.MVC.Interfaces;
using TaxFiguresCalculator.MVC.ViewModels;

namespace TaxFiguresCalculator.MVC.Services
{
    public class DataManagerViewService : IDataManagerViewService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IAsyncRepository<Transaction> _asyncTransactionRepository;
        private readonly IAsyncRepository<Customer> _customerRepositoru;
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        /// Transaction Data Manager Service for creating and persisting Transaction related Data View 
        /// Model Logic.
        /// </summary>
        /// <param name="_asyncTransactionRepository"></param>
        /// <param name="accountsrepository"></param>
        /// <param name="asyncTransactionRepository"></param>
        public DataManagerViewService(
                  IAsyncRepository<Transaction> asyncTransactionRepository,
                  IAsyncRepository<Customer> accountsrepository,
                  IAccountRepository asyncAccountRepository, 
                  IRepository<Transaction> transactionRepository)
        {
            _asyncTransactionRepository = asyncTransactionRepository;
            _customerRepositoru = accountsrepository;
            _accountRepository = asyncAccountRepository;
            _transactionRepository = transactionRepository;
        }


        /// <summary>
        /// Accepts request parameters for Customer and Transaction Table Specific attributs.
        /// Construct Model by Accesing Transaction Repository and retrieving related Entities.
        /// Returns Model.
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="transactionItems"></param>
        /// <returns></returns>
        public async Task<TransactionsDataViewModel> GetTransactionsByCustomerAsync(int CustomerId, int pageIndex, int transactionItems)
        {
            PaginationInfoViewModel PaginationInfo;

            var accountsList = await _accountRepository.GetByIdWithAccountsAsync(CustomerId);

            var transactionsList = accountsList.Where(x => x.customerId == CustomerId).
                SelectMany(x => x.Transactions).ToList();


            var sortedByPageTransactions = transactionsList
                .Skip(transactionItems * pageIndex)
                .Take(transactionItems)
                .ToList();


            //Creates Transaction Table Data View Model
            TransactionsDataViewModel transactionDataViewModel = new TransactionsDataViewModel();
            transactionDataViewModel.Customerid = CustomerId;
            transactionDataViewModel.CustomerName = accountsList.FirstOrDefault().Customer.Name + " " + accountsList.FirstOrDefault().Customer.Surname;
            transactionDataViewModel.TotalAcctouns = accountsList.Count();
            transactionDataViewModel.TotalTransactions = transactionsList.Count();

            List<TransactionViewModel> vm =
                     sortedByPageTransactions.Select(i => new TransactionViewModel()
                     {
                         TransactionId = i.Id.ToString(),
                         accountName = i.AccountId.ToString(),
                         Description = i.Description,
                         Currency = i.CurrencyCode.ToString(),
                         Amount = i.Amount.ToString()
                     }).ToList();


            ///Creates Paging Model
            PaginationInfo = new PaginationInfoViewModel()
            {
                ActualPage = pageIndex,
                ItemsPerPage = sortedByPageTransactions.Count(),
                TotalItems = transactionsList.Count(),
                TotalPages = int.Parse(Math.Ceiling(((decimal)transactionsList.Count() / transactionItems)).ToString()),
                CustomerId = CustomerId.ToString()

            };

            transactionDataViewModel.transactionViewModels = vm;
            transactionDataViewModel.PaginationInfo = PaginationInfo;

            return transactionDataViewModel;
        }


        /// <summary>
        /// Accepts Transaction ID.
        /// Retrieves Account Related Transactions.
        /// Build Transaction Model.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionViewModel GetTransactionViewModel(int id)
        {
            ///Needing better Account retrieval through Repository DI.
            var transactions = _transactionRepository.ListAll();
            var transaction = transactions.Where(x => x.Id == id).FirstOrDefault();
            var AccountId = transaction.AccountId;
            var account = _accountRepository.GetAccountById(AccountId);
            var transactionViewModel = new TransactionViewModel()
            {
                CustomerId = (int)account.customerId,
                TransactionId = transaction.Id.ToString(),
                accountName = AccountId,
                Description = transaction.Description,
                Amount = transaction.Amount.ToString(),
                Currency = transaction.CurrencyCode
            };
            return transactionViewModel;
        }

    }
}
