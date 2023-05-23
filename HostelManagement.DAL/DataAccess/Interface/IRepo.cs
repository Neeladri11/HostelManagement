using System.Linq.Expressions;

namespace HostelManagement.DAL.DataAccess.Interface
{
    public interface IRepo<T> where T : class
    {
        void AddAsync(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void UpdateExisting(T entity);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAllListAsync(Expression<Func<T, bool>> filter);
    }
}
