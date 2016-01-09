using Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly SampleContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(SampleContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null)
        {
            var query = FilterLogic(filter, orderBy, includeProperties, page, pageSize);
            return query.ToList();
        }

        public async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null)
        {
            var query = FilterLogic(filter, orderBy, includeProperties, page, pageSize);
            return await query.ToListAsync();
        }

        private IQueryable<T> FilterLogic(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties, int? page, int? pageSize)
        {
            IQueryable<T> query = _dbSet;

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page.HasValue && pageSize.HasValue)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            return query;
        }

        // Last resort!
        // It's best practice to not rely on your ORM to implement IQueryable
        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public T Create()
        {
            var entity = _dbSet.Create<T>();
            return entity;
        }

        public T GetByKey(params object[] key)
        {
            return _dbSet.Find(key);
        }

        public async Task<T> GetByKeyAsync(params object[] key)
        {
            return await _dbSet.FindAsync(key);
        }

        public T Insert(T entity)
        {
            return _dbSet.Add(entity);
        }

        public void DeleteByKey(params object[] key)
        {
            var entityToDelete = _dbSet.Find(key);

            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);

            _dbSet.Remove(entityToDelete);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public int Count(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            return await query.CountAsync();
        }
    }
}
