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
    public class DistrictRepository : GenericRepository<DistrictEntity>, IDistrictRepository
    {
        private readonly BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DistrictRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor) : base(context, sieveProcessor, sessionManager, httpContextAccessor)
        {
            _sieveProcessor = sieveProcessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public override IQueryable<DistrictEntity> GetAll(DataFilterModel dataFilterModel)
        {
            IQueryable<DistrictEntity> data = _sieveProcessor.Apply<DistrictEntity>(
                dataFilterModel,
                _context.Set<DistrictEntity>().Where(x => x.IsDeleted == false),
                applyPagination: false);

            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", data.Count().ToString());
            _httpContextAccessor.HttpContext.Response.Headers.Add("access-control-expose-headers", "X-Total-Count");

            return _sieveProcessor.Apply<DistrictEntity>(dataFilterModel, data);
        }

        public override IQueryable<DistrictEntity> Where(Expression<Func<DistrictEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
