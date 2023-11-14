using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Repositoriess.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();

        TEntity GetById(int id);

        bool Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(int id);

        int Save();
    }
}
