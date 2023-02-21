using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository.GenericRepository;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class CarRepository : GenericRepository<CarEntity>, ICarRepository
    {
        AppDbContext _appDbContext;
        public CarRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<CarEntity> GetAll()
        {
            return _context.Set<CarEntity>().Where(x => x.IsDeleted == false).Include(x => x.CarParameter);
        }
        public override IQueryable<CarEntity> Where(Expression<Func<CarEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.CarParameter);
        }
    }
}
