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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Repository.Repositories
{
    public class PersonnelRoleRepository : GenericRepository<PersonnelRoleEntity>, IPersonnelRoleRepository
    {
        AppDbContext _appDbContext;
        public PersonnelRoleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<PersonnelRoleEntity> GetAll()
        {
            return _context.Set<PersonnelRoleEntity>().Where(x => x.IsDeleted == false).Include(x => x.Personnel).Include(x=> x.Role);
        }
        public override IQueryable<PersonnelRoleEntity> Where(Expression<Func<PersonnelRoleEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.Personnel).Include(x => x.Role);
        }
    }
}
