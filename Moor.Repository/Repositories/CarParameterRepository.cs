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
    public class CarParameterRepository : GenericRepository<CarParameterEntity>, ICarParameterRepository
    {
        private readonly BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CarParameterRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor) : base(context, sieveProcessor, sessionManager, httpContextAccessor)
        {
            _sieveProcessor = sieveProcessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public override IQueryable<CarParameterEntity> GetAll(DataFilterModel dataFilterModel)
        {
            IQueryable<CarParameterEntity> data = _sieveProcessor.Apply<CarParameterEntity>(
                dataFilterModel,
                _context.Set<CarParameterEntity>().Where(x => x.IsDeleted == false).Include(x => x.CarBrand).Include(x=> x.CarModel),
                applyPagination: false);

            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", data.Count().ToString());

            return _sieveProcessor.Apply<CarParameterEntity>(dataFilterModel, data);
        }

        public override IQueryable<CarParameterEntity> Where(Expression<Func<CarParameterEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.CarBrand).Include(x => x.CarModel);
        }
    }
}
