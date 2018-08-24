using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxFiguresCalculator.Core.Model.Entities;
//using TaxFiguresCalculator.Infrastracture;

namespace TaxFiguresCalculator.Core.Repositories
{
   public interface IAsyncRepository <T> where T : BaseEntity
    {
        Task<T>GetByIdAsync(int id);
        Task<List<T>> ListAllAsync();
        Task<List<T>> ListAllAsyncById(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void UpdateBulkAsync(List<T> entity);
    }
}
