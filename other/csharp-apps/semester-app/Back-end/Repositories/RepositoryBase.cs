using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public DataContext _dataContext { get; set; }

        public RepositoryBase(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Create(T entity)
        {
            _dataContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return _dataContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dataContext.Set<T>().Where(expression).AsNoTracking();
        }

        public async Task<List<T>> GetAll()
        {
            return await FindAll().ToListAsync();
        }

        public void Update(T entity)
        {
            _dataContext.Set<T>().Update(entity);
        }
    }
}
