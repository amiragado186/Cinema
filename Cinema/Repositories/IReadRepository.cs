using System.Linq.Expressions;

namespace Cinema.Repositories
{
    public interface IReadRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            bool tracked = false,
            params Expression<Func<T, object>>[] includes);

        Task<T?> GetOneAsync(
            Expression<Func<T, bool>> filter,
            bool tracked = true,
            params Expression<Func<T, object>>[] includes);
    }
}