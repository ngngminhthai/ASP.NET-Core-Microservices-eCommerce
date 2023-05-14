﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Contracts.Domains.Interfaces
{
    public interface IRepositoryBase<T, K, TContext>
        where T : EntityBase<K>
        where TContext : DbContext
    {
        IQueryable<T> GetAll(bool isAsNoTracking = false, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        IQueryable<T> ExecuteSqlQuery(string query);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task InsertAsync(T entity);
        Task InsertIfNotExistAsync(Expression<Func<T, Guid>> identifierExpression, List<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteAsync(K id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task UpdateAsync(T entity);
        Task UpdateArrange(IEnumerable<T> entities);
       

        Task Save();
    }
}