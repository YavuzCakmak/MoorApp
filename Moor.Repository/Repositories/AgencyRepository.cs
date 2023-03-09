using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Session;
using Moor.Core.Sieve;
using Moor.Core.Utilities.DataFilter;
using Moor.Repository.GenericRepository;
using Sieve.Models;
using System;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class AgencyRepository : GenericRepository<AgencyEntity>, IAgencyRepository
    {
        private readonly BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AgencyRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor) : base(context, sieveProcessor, sessionManager, httpContextAccessor)
        {
            _sieveProcessor = sieveProcessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public override IQueryable<AgencyEntity> GetAll(DataFilterModel dataFilterModel)
        {
            IQueryable<AgencyEntity> data = _sieveProcessor.Apply<AgencyEntity>(
                dataFilterModel,
                _context.Set<AgencyEntity>().Where(x => x.IsDeleted == false).Include(x => x.Personnel),
                applyPagination: false);

            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", data.Count().ToString());

            return _sieveProcessor.Apply<AgencyEntity>(dataFilterModel, data);
        }

        public override IQueryable<AgencyEntity> Where(Expression<Func<AgencyEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.Personnel);
        }
    }
}
