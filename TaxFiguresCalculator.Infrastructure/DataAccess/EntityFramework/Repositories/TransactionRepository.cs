using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Repositories;
using TaxFiguresCalculator.Infrastracture.DataAccess;

namespace TaxFiguresCalculator.Infrastructure.DataAccess
{
    public class TransactionRepository : EfRepository<Transaction>, Core.Repositories.ITransactionRepository
    {
        public TransactionRepository(Tax_Figures_CalculatorContext dbContext) : base(dbContext)
        {
        }

        public Task<Transaction> GetByIdCompositeAsync(long id, string key)
        {
            return _dbContext
                .Transaction.Where(t => t.Id == id && t.AccountId == key).FirstOrDefaultAsync();
        }
    }
}
    
