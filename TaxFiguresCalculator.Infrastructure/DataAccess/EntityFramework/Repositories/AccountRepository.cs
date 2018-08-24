using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Repositories;

namespace TaxFiguresCalculator.Infrastracture.DataAccess
{
   public class AccountRepository: EfRepository<Account>, IAccountRepository
    {
        public AccountRepository(Tax_Figures_CalculatorContext dbContext) : base(dbContext)
        {
        }
        public Customer GetByIdWithAccounts(int id)
        {
            return _dbContext.Customer
                .Include(c => c.Accounts)
                .Where(x=>x.Id==id)
                .FirstOrDefault();
        }

        public Task<List<Account>> GetByIdWithAccountsAsync(int id)
        {
            return _dbContext.Account
                .Include(c=>c.Customer)
                .Include(o => o.Transactions).ToListAsync();
        }

        public Account GetAccountByIdWithTransactions()
        {
            return _dbContext.Account
                .Include(c=>c.Transactions)
                .FirstOrDefault();
        }

        public Task<Account> GetAccountByIdWithTransactionsAsync()
        {
            return _dbContext.Account
               .Include(c => c.Transactions)
               .FirstOrDefaultAsync();
        }

        public Task<List<Account>> GetAccountsByCustomer(int id)
        {
            return _dbContext.Account
                   .Where(x => x.customerId == id)
                   .ToListAsync();
        }

        public Task<List<Account>> GetAccountsByclient(int id)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountById(string id)
        {
            return _dbContext.Account.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
