using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository.GenericRepository;

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
            return _context.Set<CarEntity>().Include(x => x.CarParameter);
        }
    }
}
