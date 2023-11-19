using Hamburgerci.Entities.Abstract;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Hamburgerci.Repositories.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T> where T :class, IBaseEntity
    {
        private readonly AppDbContext _context;
        private DbSet<T> table;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
		}

		public async Task<bool> Any(Expression<Func<T, bool>> expression)
		{
			return await table.AnyAsync(expression);
		}

		public async Task Create(T entity)
		{
			await table.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(T entity)
		{
			await _context.SaveChangesAsync();
		}

		public async Task<T> GetDefault(Expression<Func<T, bool>> expression)
		{
			return await table.FirstOrDefaultAsync(expression);
		}

		//Todo: Implement this method
		public async Task<IQueryable<T>> GetDefaults(Expression<Func<T, bool>> expression)
		{
			return table.Where(expression);
		}

		public async Task<TResult> GetFilteredFirstOrDefault<TResult>(
			Expression<Func<T, TResult>> select,
			Expression<Func<T, bool>> where,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
		{
			IQueryable<T> query = table; // select * from table
			if (where != null)
				query = query.Where(where); // select * from table where Id=3

			if (include != null)
				query = include(query); // select * from table where Id=3 Entity

			if (orderBy != null)
				return await orderBy(query).Select(select).FirstOrDefaultAsync(); // select * from table where Id=3 Entity order by Id desc
																				  //    query = orderBy(query); // select * from table where Id=3 Entity order by Id desc
			else
				return await query.Select(select).FirstOrDefaultAsync();
			//return await query.Select(select).FirstOrDefaultAsync();
		}

		public async Task<ICollection<TResult>> GetFilteredList<TResult>(
			Expression<Func<T, TResult>> select,
			Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
		{
			IQueryable<T> query = table; // select * from table
			if (where != null)
				query = query.Where(where); // select * from table where Id=3

			if (include != null)
				query = include(query); // select * from table where Id=3 Entity

			if (orderBy != null)
				return await orderBy(query).Select(select).ToListAsync(); // select * from table where Id=3 Entity order by Id desc
																		  //    query = orderBy(query); // select * from table where Id=3 Entity order by Id desc
			else
				return await query.Select(select).ToListAsync();
			//return await query.Select(select).ToListAsync();
		}

		public async Task Update(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
