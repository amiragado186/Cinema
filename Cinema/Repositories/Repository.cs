using System.Linq.Expressions;
using Cinema.DataAcess;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public Repository()
        {
        }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            bool tracked = false,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetOneAsync(
            Expression<Func<T, bool>> filter,
            bool tracked = true,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public static implicit operator Repository<T>(Movie v)
        {
            throw new NotImplementedException();
        }
    }
}