
namespace Hamburgerci.Repository.Abstract
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
