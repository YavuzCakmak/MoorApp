using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Core.Repositories.MoorRepository.AuthorizeRepository;
using Moor.Repository.GenericRepository;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Repository.Repositories
{
    public class PrivilegeRepository : GenericRepository<PrivilegeEntity>, IPrivilegeRepository
    {
        AppDbContext _appDbContext;
        public PrivilegeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<PrivilegeEntity> GetAll()
        {
            return _context.Set<PrivilegeEntity>().Where(x => x.IsDeleted == false);
        }
        public override IQueryable<PrivilegeEntity> Where(Expression<Func<PrivilegeEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
