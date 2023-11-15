using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Hamburgerci.Repositories.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T> where T :class, Entities.Abstract.IBaseEntity
    {
        private readonly AppDbContext _context;
        private DbSet<T> _table;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _table.AddAsync(entity);
            return await SaveAsync() > 0;
        }

        public bool Delete(int id)
        {
            _table.Remove(GetById(id));
            return Save() > 0;
        }

        public IQueryable<T> GetAll()
        {
            return _table;
        }

        public T GetById(int id)
        {
            return _table.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public bool Update(T entity)
        {
            EntityEntry<T> entityEntry = _table.Update(entity);
            entityEntry.State = EntityState.Modified;

            return Save() > 0;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            await _table.AddRangeAsync(entities);
            return await SaveAsync() > 0;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await _table.FirstOrDefaultAsync(expression);
        }

        public bool Remove(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            EntityEntry entityEntry= _table.Update(entity);
            entityEntry.State = EntityState.Deleted;
            return Save() > 0;
        }

        public bool RemoveRange(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                Remove(item);
            }
            return true;
        }
    }
}
