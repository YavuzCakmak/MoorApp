using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository.GenericRepository;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class PriceRepository : GenericRepository<PriceEntity>, IPriceRepository
    {
        AppDbContext _appDbContext;
        public PriceRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }
        public override IQueryable<PriceEntity> GetAll()
        {
            return _context.Set<PriceEntity>().Where(x => x.IsDeleted == false).Include(x => x.District).Include(x => x.CarParameter);
        }
        public override IQueryable<PriceEntity> Where(Expression<Func<PriceEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.District).Include(x => x.CarParameter);
        }
    }
}
