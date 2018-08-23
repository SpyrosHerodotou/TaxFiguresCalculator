using System;
using System.Collections.Generic;
using System.Text;
using TaxFiguresCalculator.Core.Model.Entities;

namespace TaxFiguresCalculator.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Update(T entity);
         T GetById(int id);
       T GetByIdComposite(long id, string key);
        void Delete(T entity);
        List<T> ListAll();
    }
}
