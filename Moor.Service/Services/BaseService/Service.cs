using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.Base;
using Moor.Core.Repositories;
using Moor.Core.Services.BaseService;
using Moor.Core.UnitOfWorks;
using Moor.Core.Utilities.DataFilter;
using Moor.Service.Exceptions;
using System.Linq.Expressions;

namespace Moor.Service.Services.BaseService
{
    public class Service<T> : IService<T> where T : CoreEntity
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(DataFilterModel dataFilterModel)
        {
            return await _repository.GetAll(dataFilterModel).ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            var value = await _repository.GetByIdAsync(id);
            if (value == null)
            {
                throw new ClientSideException($"{typeof(T).Name} not found");
            }
            return value;
        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
