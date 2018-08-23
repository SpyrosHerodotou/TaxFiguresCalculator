using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxFiguresCalculator.Core.Model.Entities;
using TaxFiguresCalculator.Core.Repositories;

namespace TaxFiguresCalculator.Infrastracture.DataAccess
{
    public class EfRepository<T> : IRepository<T>, IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly Tax_Figures_CalculatorContext _dbContext;

        public EfRepository(Tax_Figures_CalculatorContext dbContext) { 
            _dbContext = dbContext;
        }

        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public virtual T GetByIdComposite(long id,string key)
        {
            return _dbContext.Set<T>().Find(id,key);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
           long ids = (long)id;
            return await _dbContext.Set<T>().FindAsync(ids);
        }

        public IEnumerable<T> ListAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public async Task<List<T>> ListAllAsync()
        {
            return  _dbContext.Set<T>().ToList();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async virtual void UpdateBulkAsync(List<T> entity)
        {
            _dbContext.BulkInsertAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> ListAllAsyncById(int id)
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        List<T> IRepository<T>.ListAll()
        {
            return _dbContext.Set<T>().ToList();
        }
    }
}
