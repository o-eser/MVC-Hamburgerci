using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Abstract;
using Hamburgerci.Entities.Enum;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Services.Abstract;

namespace Hamburgerci.Services.Concrete
{
    public class BaseManager<T> : IService<T> where T : class, IBaseEntity
    {
        private readonly IBaseRepository<T> _baseRepository;

        public BaseManager(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<T> CreateAsync(T entity)
        {
            if (entity is not null)
            {
                await _baseRepository.AddAsync(entity);
                await _baseRepository.SaveAsync();
                return entity;
            }
            else
                throw new Exception(entity.GetType().Name + "is null");
        }

        public void Delete(int id)
        {
            if (id > 0)
            {
                _baseRepository.Delete(id);
                _baseRepository.Save();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return GetWhere(e => e.DataStatus != DataStatus.Deleted);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (id > 0)
                return await _baseRepository.GetByIdAsync(id);
            else
                throw new Exception("Id 0 dan büyük olmalıdır");
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await _baseRepository.GetSingleAsync(expression);
        }

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> expression)
        {
            return _baseRepository.GetWhere(expression);
        }

        public async Task RemoveAsync(int id)
        {
            if (id > 0)
            {
                T entity = await GetByIdAsync(id);
                entity.DataStatus = DataStatus.Deleted;
                entity.DeletedDate = DateTime.Now;
                 Update(entity);

                await _baseRepository.SaveAsync();
            }
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new Exception(entity.GetType().Name + " is null");
            }
            //else if (entity.Id == null)
            //{
            //    throw new Exception(entity.GetType().Name + " ID null");
            //}
            else
            {
                _baseRepository.Update(entity);
                _baseRepository.Save();
                return entity;
            }
        }
    }
}
