using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository.GenericRepository;

namespace Moor.Repository.Repositories
{
    public class CarRepository : GenericRepository<CarEntity>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context)
        {
        }
    }
}
