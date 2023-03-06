using Moor.Core.Entities.Base;
using Moor.Core.Utilities.DataFilter;
using System.Linq.Expressions;

namespace Moor.Core.Services.BaseService
{
    public interface IService<T> where T : CoreEntity
    {
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync(DataFilterModel dataFilterModel);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
