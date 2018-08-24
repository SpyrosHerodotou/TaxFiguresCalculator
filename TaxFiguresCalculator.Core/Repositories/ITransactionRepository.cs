using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxFiguresCalculator.Core.Entities;
using TaxFiguresCalculator.Core.Interfaces;
using TaxFiguresCalculator.Core.Services;

namespace TaxFiguresCalculator.Core.Repositories
{
   public interface ITransactionRepository: IAsyncRepository<Transaction>, IRepository<Transaction>
    {
        Task<Transaction> GetByIdCompositeAsync(long id, string key);

    }
}
