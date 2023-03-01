using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository.GenericRepository;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class CountyRepository : GenericRepository<CountyEntity>, ICountyRepository
    {
        AppDbContext _appDbContext;
        public CountyRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<CountyEntity> GetAll()
        {
            return _context.Set<CountyEntity>().Where(x => x.IsDeleted == false).Include(x=> x.City).ThenInclude(x=> x.Country);
        }
        public override IQueryable<CountyEntity> Where(Expression<Func<CountyEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.City).ThenInclude(x => x.Country);
        }
    }
}
