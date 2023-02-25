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
    public class RoleRepository : GenericRepository<RoleEntity>, IRoleRepository
    {
        AppDbContext _appDbContext;
        public RoleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<RoleEntity> GetAll()
        {
            return _context.Set<RoleEntity>().Where(x => x.IsDeleted == false);
        }
        public override IQueryable<RoleEntity> Where(Expression<Func<RoleEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
