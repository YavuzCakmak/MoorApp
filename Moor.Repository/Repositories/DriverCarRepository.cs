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
    public class DriverCarRepository : GenericRepository<DriverCarEntity>, IDriverCarRepository
    {
        private readonly BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DriverCarRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor) : base(context, sieveProcessor, sessionManager, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _sieveProcessor = sieveProcessor;
        }

        public override IQueryable<DriverCarEntity> GetAll(DataFilterModel dataFilterModel)
        {
            IQueryable<DriverCarEntity> data = _sieveProcessor.Apply<DriverCarEntity>(
                dataFilterModel,
                _context.Set<DriverCarEntity>().Where(x => x.IsDeleted == false).Include(x => x.Driver).Include(x => x.Car),
                applyPagination: false);

            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", data.Count().ToString());

            return _sieveProcessor.Apply<DriverCarEntity>(dataFilterModel, data);
        }

        public override IQueryable<DriverCarEntity> Where(Expression<Func<DriverCarEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x=> x.Driver).Include(x=> x.Car);
        }
    }
}
