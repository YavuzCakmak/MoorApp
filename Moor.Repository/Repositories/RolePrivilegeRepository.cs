using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories.MoorRepository.AuthorizeRepository;
using Moor.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Repository.Repositories
{
    public class RolePrivilegeRepository : GenericRepository<RolePrivilegeEntity>, IRolePrivilegeRepository
    {
        AppDbContext _appDbContext;
        public RolePrivilegeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<RolePrivilegeEntity> GetAll()
        {
            return _context.Set<RolePrivilegeEntity>().Where(x => x.IsDeleted == false).Include(x => x.Privilege).Include(x=> x.Role);
        }
        public override IQueryable<RolePrivilegeEntity> Where(Expression<Func<RolePrivilegeEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.Privilege).Include(x => x.Role);
        }
    }
}
