using Hamburgerci.Repositoriess.Abstract;
using Hamburgerci.Repositoriess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Repositoriess.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> _table;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public bool Add(T entity)
        {
            _context.Add(entity);
            return Save() > 0;
        }

        public bool Delete(int id)
        {
            _context.Remove(GetById(id));
            return Save() > 0;
        }

        public List<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetById(int id)
        {
            return _table.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            return Save() > 0;
        }
    }
}
