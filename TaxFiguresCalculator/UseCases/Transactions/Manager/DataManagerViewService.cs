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
        private readonly IAsyncRepository<Transaction> _itemrepository;
        private readonly IAsyncRepository<Customer> _accountsrepository;
        private readonly IAccountRepository _transactionrepository;

        public DataManagerViewService(
                  IAsyncRepository<Transaction> itemrepository,
                  IAsyncRepository<Customer> accountsrepository,
                  IAccountRepository transactionrepository)
        {
            _itemrepository = itemrepository;
            _accountsrepository = accountsrepository;
            _transactionrepository = transactionrepository;
        }

        public PaginationInfoViewModel PaginationInfo { get; private set; }

        public async Task<TransactionDetailsViewModel> GetTransactionsByCustomerAsync(int customerId, int pageIndex, int itemsPage)
        {

            var roots = await _transactionrepository.GetByIdWithAccountsAsync(customerId);
            var totalItems = roots.Where(x=>x.CustomerId==customerId);
            var root = totalItems.SelectMany(x => x.Transactions).ToList();
            var itemsOnPage = root
                .Skip(itemsPage * pageIndex)
                .Take(itemsPage)
                .ToList();

            TransactionDetailsViewModel transactionDetailsView = new TransactionDetailsViewModel();
            transactionDetailsView.CustomerName = roots.FirstOrDefault().Customer.Name + " " + roots.FirstOrDefault().Customer.Surname;
            transactionDetailsView.TotalAcctouns = roots.Count();
            transactionDetailsView.TotalTransactions = root.Count();
            List<TransactionViewModel> vm =
                     itemsOnPage.Select(i => new TransactionViewModel()
                     {
                         TransactionId = i.Id.ToString(),
                         accountName = i.AccountId.ToString(),
                         Description = i.Description,
                         Currency = i.CurrencyCode.ToString(),
                         Amount = i.Amount.ToString()
                     }).ToList();

            PaginationInfo = new PaginationInfoViewModel()
            {
                ActualPage = pageIndex,
                ItemsPerPage = itemsOnPage.Count,
                TotalItems = root.Count(),
                TotalPages = int.Parse(Math.Ceiling(((decimal)root.Count() / itemsPage)).ToString()),
                customerId = customerId.ToString()
                
            };
            transactionDetailsView.transactionViewModels = vm;
            transactionDetailsView.PaginationInfo = PaginationInfo;
            return transactionDetailsView;
        }

        public TransactionViewModel GetTransactionViewModel(int id)
        {
            var account = _transactionrepository.GetAccountByIdWithTransactions();
            var AccountId = account.Id;
            var transaction = account.Transactions.Where(x => x.Id == id).FirstOrDefault();
            var transactionViewModel = new TransactionViewModel()
            {
                CustomerId = (int)account.CustomerId,
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
