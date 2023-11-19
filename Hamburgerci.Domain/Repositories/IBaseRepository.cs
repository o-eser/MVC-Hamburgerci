﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Hamburgerci.Repositories.Abstract
{
    public interface IBaseRepository<T> where T :class, Entities.Abstract.IBaseEntity
    {
		Task Create(T entity);
		Task Update(T entity);
		Task Delete(T entity);
		Task<bool> Any(Expression<Func<T, bool>> expression);
		Task<T> GetDefault(Expression<Func<T, bool>> expression);
		Task<IQueryable<T>> GetDefaults(Expression<Func<T, bool>> expression);
		Task<TResult> GetFilteredFirstOrDefault<TResult>(
			Expression<Func<T, TResult>> select, //select(x=>new {x.Id,x.Name})
			Expression<Func<T, bool>> where,  //where(x=>x.Id==1)
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, //orderBy(x=>x.Id)
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null); //include(x=>x.Category) Join
		Task<ICollection<TResult>> GetFilteredList<TResult>(
			Expression<Func<T, TResult>> select, //select(x=>new {x.Id,x.Name})
			Expression<Func<T, bool>> where,  //where(x=>x.Id==1)
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, //orderBy(x=>x.Id)
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null); //include(x=>x.Category) Join


	}
}
