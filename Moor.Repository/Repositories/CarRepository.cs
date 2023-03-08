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
using static Sieve.Extensions.MethodInfoExtended;

namespace Moor.Repository.Repositories
{
    public class CarRepository : GenericRepository<CarEntity>, ICarRepository
    {
        private readonly BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CarRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor) : base(context, sieveProcessor, sessionManager, httpContextAccessor)
        {
            _sieveProcessor = sieveProcessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public override IQueryable<CarEntity> GetAll(DataFilterModel dataFilterModel)
        {
            IQueryable<CarEntity> data = _sieveProcessor.Apply<CarEntity>(
                dataFilterModel, 
                _context.Set<CarEntity>().Where(x => x.IsDeleted == false).Include(x => x.CarParameter).ThenInclude(x=> x.CarBrand).Include(x=> x.CarParameter).ThenInclude(x=> x.CarModel),
                applyPagination: false);

            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", data.Count().ToString());

            return _sieveProcessor.Apply<CarEntity>(dataFilterModel, data);
        }
        public override IQueryable<CarEntity> Where(Expression<Func<CarEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.CarParameter);
        }
    }
}
