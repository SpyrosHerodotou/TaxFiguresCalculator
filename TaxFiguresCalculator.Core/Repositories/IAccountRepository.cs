using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Services;

namespace TaxFiguresCalculator.Core.Repositories
{
   public interface IAccountRepository:IAsyncRepository<Account>,IRepository<Account>
    {
        Customer GetByIdWithAccounts(int id);
        Task<List<Account>> GetByIdWithAccountsAsync(int id);
        Account GetAccountByIdWithTransactions();
       Task<Account> GetAccountByIdWithTransactionsAsync();
        Task<List<Account>> GetAccountsByCustomer(int id);

    }
}
