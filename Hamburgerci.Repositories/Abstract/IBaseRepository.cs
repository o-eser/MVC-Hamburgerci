using System.Linq.Expressions;

namespace Hamburgerci.Repositories.Abstract
{
    public interface IBaseRepository<T> where T :class, Entities.Abstract.IBaseEntity
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
