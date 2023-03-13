using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Session;
using Moor.Core.Sieve;
using Moor.Core.Utilities.DataFilter;
using Moor.Repository.GenericRepository;
using Sieve.Models;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class PriceRepository : GenericRepository<PriceEntity>, IPriceRepository
    {
        private readonly BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PriceRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor) : base(context, sieveProcessor, sessionManager, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _sieveProcessor = sieveProcessor;
        }

        public override IQueryable<PriceEntity> GetAll(DataFilterModel dataFilterModel)
        {
            IQueryable<PriceEntity> data = _sieveProcessor.Apply<PriceEntity>(
                dataFilterModel,
                _context.Set<PriceEntity>().Where(x => x.IsDeleted == false).Include(x => x.District).Include(x => x.CarParameter),
                 applyPagination: false);

            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", data.Count().ToString());
            _httpContextAccessor.HttpContext.Response.Headers.Add("access-control-expose-headers", "X-Total-Count");


            return _sieveProcessor.Apply<PriceEntity>(dataFilterModel, data);
        }

        public override IQueryable<PriceEntity> Where(Expression<Func<PriceEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.District).Include(x => x.CarParameter);
        }
    }
}
