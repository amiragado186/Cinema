using System.Linq.Expressions;

namespace Cinema.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
       where T : class
    {
        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            bool tracked = false,
            params Expression<Func<T, object>>[] includes);

        Task<T?> GetOneAsync(
            Expression<Func<T, bool>> filter,
            bool tracked = true,
            params Expression<Func<T, object>>[] includes);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        Task CommitAsync();
    }
}
