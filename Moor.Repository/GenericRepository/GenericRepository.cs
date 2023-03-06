using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.Base;
using Moor.Core.Repositories;
using Moor.Core.Session;
using Moor.Core.Sieve;
using Moor.Core.Utilities.DataFilter;
using Sieve.Models;
using System.Linq.Expressions;

namespace Moor.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : CoreEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> _sieveProcessor;
        private readonly SessionManager _sessionManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _sieveProcessor = sieveProcessor;
            _sessionManager = sessionManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task AddAsync(T entity)
        {
           var aa = await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public virtual IQueryable<T> GetAll(DataFilterModel dataFilterModel)
        {
            IQueryable<T> data;

            try
            {
                data = _sieveProcessor.Apply<T>(dataFilterModel, _dbSet.AsNoTracking().Where(x => !x.IsDeleted), applyPagination: false);

                _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", data.Count().ToString());

                data = _sieveProcessor.Apply<T>(dataFilterModel, data);
            }
            catch(Exception ex)
            {
                throw ex;// silersen data hata verir.(use of unasigned local variable)
               // core katmanına özgü bir exception yazılıp atılabilir.
            }

            return data;
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
