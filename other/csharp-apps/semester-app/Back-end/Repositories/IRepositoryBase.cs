using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<List<T>> GetAll();
    }
}
