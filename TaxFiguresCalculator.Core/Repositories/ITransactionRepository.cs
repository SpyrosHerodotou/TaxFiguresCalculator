using System;
using System.Collections.Generic;
using System.Text;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Interfaces;
using TaxFiguresCalculator.Core.Services;

namespace TaxFiguresCalculator.Core.Repositories
{
   public interface ITransactionRepository: IAsyncRepository<Transaction>, IRepository<Transaction>
    {
        Transaction GetByIdComposite(long id, string key);
    }
}
