using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces.Repositories.Base;

namespace Web.Infrastructure.Repositories.Base
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly WebDbContext _context;
        private readonly DbSet<T> _dbSet;

        protected Repository(WebDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return await _dbSet.AnyAsync(predicate!);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return await _dbSet.CountAsync(predicate!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _dbSet.Remove(entity);
            return true;

        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbSet.Where(predicate).AsQueryable();
            if (includes != null)
                query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbSet.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate).AsQueryable();
            }
            if (includes != null)
                query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync();
        }

        public IQueryable<T> GetAllQuerable(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbSet.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate).AsQueryable();
            }
            if (includes != null)
                query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query;
        }

        public async Task<T?> GetAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _dbSet.FindAsync(id);
        }

        public T UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbSet.Attach(entity);
            return entity;
        }
    }
}
