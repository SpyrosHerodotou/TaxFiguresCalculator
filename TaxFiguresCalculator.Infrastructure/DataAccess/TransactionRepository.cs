using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Repositories;
using TaxFiguresCalculator.Infrastracture.DataAccess;

namespace TaxFiguresCalculator.Infrastructure.DataAccess
{
    public class ITransactionRepository : EfRepository<Transaction>, Core.Repositories.ITransactionRepository
    {
        public ITransactionRepository(Tax_Figures_CalculatorContext dbContext) : base(dbContext)
        {
        }
        public virtual Transaction GetByIdComposite(long id, string key)
        {
            return _dbContext
                .Transaction.Where(t => t.Id == id && t.AccountId == key).FirstOrDefault();
                   
        }
    }
}
    
