using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository.GenericRepository;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class DistrictRepository : GenericRepository<DistrictEntity>, IDistrictRepository
    {
        AppDbContext _appDbContext;
        public DistrictRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<DistrictEntity> GetAll()
        {
            return _context.Set<DistrictEntity>().Where(x => x.IsDeleted == false);
        }
        public override IQueryable<DistrictEntity> Where(Expression<Func<DistrictEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
