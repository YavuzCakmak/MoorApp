using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository.GenericRepository;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class TravellerRepository : GenericRepository<TravellerEntity>, ITravellerRepository
    {
        AppDbContext _appDbContext;
        public TravellerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }
        public override IQueryable<TravellerEntity> GetAll()
        {
            return _context.Set<TravellerEntity>().Where(x => x.IsDeleted == false);
        }
        public override IQueryable<TravellerEntity> Where(Expression<Func<TravellerEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
