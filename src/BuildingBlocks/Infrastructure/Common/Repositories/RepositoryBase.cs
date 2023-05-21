using Contracts.Domains;
using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Common.Repositories
{
    public class RepositoryBase<T, K, TContext> : IRepositoryBase<T, K, TContext>
        where T : EntityBase<K>
        where TContext : DbContext
    {
        private readonly TContext _context;
        public RepositoryBase(TContext context)
        {
            _context = context;
        }
        public async Task InsertAsync(T entity)
        {
            _context.Add(entity);
            await Save();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await Save();
        }

        public async Task DeleteAsync(K id)
        {
            await DeleteAsync(await _context.Set<T>().FindAsync(id));
            await Save();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateArrange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            await Save();
        }

        public async Task InsertIfNotExistAsync(Expression<Func<T, K>> identifierExpression, List<T> entities)
        {
            foreach (var entity in entities)
            {
                var v = identifierExpression.Compile()(entity);
                var predicate = Expression.Lambda<Func<T, bool>>(Expression.Equal(identifierExpression.Body, Expression.Constant(v)), identifierExpression.Parameters);

                var entry = await GetFirstAsync(predicate);
                if (entry == null)
                {
                    await InsertAsync(entity);
                }
            }
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> GetAll(bool isAsNoTracking = false, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (isAsNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            return query;
        }

        //public IQueryable<T> ExecuteSqlQuery(string query)
        //{
        //    return Uow.DbContext.Set<T>().FromSql(query);
        //}

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate,
                                            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (include != null)
            {
                query = include(query);
            }

            return await query.SingleOrDefaultAsync(predicate);
        }



        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> ExecuteSqlQuery(string query)
        {
            throw new NotImplementedException();
        }

        public Task InsertIfNotExistAsync(Expression<Func<T, Guid>> identifierExpression, List<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
