using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository.GenericRepository;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class AgencyRepository : GenericRepository<AgencyEntity>, IAgencyRepository
    {
        AppDbContext _appDbContext;
        public AgencyRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<AgencyEntity> GetAll()
        {
            return _context.Set<AgencyEntity>().Where(x => x.IsDeleted == false).Include(x => x.Staff);
        }
        public override IQueryable<AgencyEntity> Where(Expression<Func<AgencyEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.Staff);
        }
    }
}
