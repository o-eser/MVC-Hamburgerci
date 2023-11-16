using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Abstract;

namespace Hamburgerci.Services.Abstract
{
    public interface IService<T> where T : class, IBaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);

        Task<T> CreateAsync(T entity);
        T Update(T entity);
        Task Remove(int id);
        void Delete(int id);
    }
}
