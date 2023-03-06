using Moor.Core.Entities.Base;
using Moor.Core.Utilities.DataFilter;
using System.Linq.Expressions;

namespace Moor.Core.Repositories
{
    public interface IGenericRepository<T> where T : CoreEntity
    {
        Task<T> GetByIdAsync(long id);
        IQueryable<T> GetAll(DataFilterModel dataFilterModel);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
