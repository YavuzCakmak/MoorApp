using Microsoft.AspNetCore.Http;
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
        public AgencyRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor) : base(context, sieveProcessor, sessionManager, httpContextAccessor)
        {
        }

        public override IQueryable<AgencyEntity> Where(Expression<Func<AgencyEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
