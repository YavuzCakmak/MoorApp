using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Session;
using Moor.Core.Sieve;
using Moor.Core.Utilities.DataFilter;
using Moor.Repository.GenericRepository;
using Sieve.Models;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class CityRepository : GenericRepository<CityEntity>, ICityRepository
    {
        private readonly BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CityRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor) : base(context, sieveProcessor, sessionManager, httpContextAccessor)
        {
            _sieveProcessor = sieveProcessor;
            _httpContextAccessor= httpContextAccessor;
        }

        public override IQueryable<CityEntity> GetAll(DataFilterModel dataFilterModel)
        {
            IQueryable<CityEntity> data = _sieveProcessor.Apply<CityEntity>(
                dataFilterModel,
              _context.Set<CityEntity>().Where(x => x.IsDeleted == false).Include(x => x.Country),
                applyPagination: false);

            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", data.Count().ToString());
            _httpContextAccessor.HttpContext.Response.Headers.Add("access-control-expose-headers", "X-Total-Count");

            return _sieveProcessor.Apply<CityEntity>(dataFilterModel, data);
        }

        public override IQueryable<CityEntity> Where(Expression<Func<CityEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.Country);
        }
    }
}
