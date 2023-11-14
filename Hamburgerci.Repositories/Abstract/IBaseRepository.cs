using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Abstract;
using Hamburgerci.Entities.Concrete;

namespace Hamburgerci.Repositories.Abstract
{
    public interface IBaseRepository<T> where T :class, IBaseEntity
    {
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id);

        T GetById(int id);

        IQueryable<T> GetWhere(Expression<Func<T, bool>> expression);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);

        Task<bool> AddAsync(T entity);

        Task<bool> AddRangeAsync(IEnumerable<T> entities);

        bool Update(T entity);

        bool Remove(T entity);

        bool RemoveRange(IEnumerable<T> entities);

        bool Delete(int id);

        int Save();

        Task<int> SaveAsync();
    }
}
