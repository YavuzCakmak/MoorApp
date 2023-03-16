using Microsoft.AspNetCore.Http;
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
    public class TravellerRepository : GenericRepository<TravellerEntity>, ITravellerRepository
    {
        private readonly BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TravellerRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor) : base(context, sieveProcessor, sessionManager, httpContextAccessor)
        {
            _sieveProcessor = sieveProcessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public override IQueryable<TravellerEntity> GetAll(DataFilterModel dataFilterModel)
        {
            IQueryable<TravellerEntity> data = _sieveProcessor.Apply<TravellerEntity>(
                dataFilterModel,
                _context.Set<TravellerEntity>().Where(x => x.IsDeleted == false),
                applyPagination: false);

            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", data.Count().ToString());
            _httpContextAccessor.HttpContext.Response.Headers.Add("access-control-expose-headers", "X-Total-Count");

            return _sieveProcessor.Apply<TravellerEntity>(dataFilterModel, data);
        }

        public override IQueryable<TravellerEntity> Where(Expression<Func<TravellerEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
