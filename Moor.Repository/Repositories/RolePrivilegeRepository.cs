using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Core.Repositories.MoorRepository.AuthorizeRepository;
using Moor.Core.Session;
using Moor.Core.Sieve;
using Moor.Core.Utilities.DataFilter;
using Moor.Repository.GenericRepository;
using Sieve.Models;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class RolePrivilegeRepository : GenericRepository<RolePrivilegeEntity>, IRolePrivilegeRepository
    {
        private readonly BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RolePrivilegeRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor) : base(context, sieveProcessor, sessionManager, httpContextAccessor)
        {
            _sieveProcessor = sieveProcessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public override IQueryable<RolePrivilegeEntity> GetAll(DataFilterModel dataFilterModel)
        {
            IQueryable<RolePrivilegeEntity> data = _sieveProcessor.Apply<RolePrivilegeEntity>(
                dataFilterModel,
                _context.Set<RolePrivilegeEntity>().Where(x => x.IsDeleted == false).Include(x => x.Privilege).Include(x => x.Role),
                applyPagination: false);

            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", data.Count().ToString());

            return _sieveProcessor.Apply<RolePrivilegeEntity>(dataFilterModel, data);
        }

        public override IQueryable<RolePrivilegeEntity> Where(Expression<Func<RolePrivilegeEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.Privilege).Include(x => x.Role);
        }
    }
}
