using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public class PersonnelRoleRepository : GenericRepository<PersonnelRoleEntity>, IPersonnelRoleRepository
    {
        private readonly BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PersonnelRoleRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor) : base(context, sieveProcessor, sessionManager, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _sieveProcessor  = sieveProcessor;
        }

        public override IQueryable<PersonnelRoleEntity> GetAll(DataFilterModel dataFilterModel)
        {
            IQueryable<PersonnelRoleEntity> data = _sieveProcessor.Apply<PersonnelRoleEntity>(
                dataFilterModel,
                _context.Set<PersonnelRoleEntity>().Where(x => x.IsDeleted == false).Include(x => x.Personnel).Include(x => x.Role),
                 applyPagination: false);

            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", data.Count().ToString());

            return _sieveProcessor.Apply<PersonnelRoleEntity>(dataFilterModel, data);
        }

        public override IQueryable<PersonnelRoleEntity> Where(Expression<Func<PersonnelRoleEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.Personnel).Include(x => x.Role);
        }
    }
}
