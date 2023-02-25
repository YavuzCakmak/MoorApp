using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Core.Repositories.MoorRepository.AuthorizeRepository;
using Moor.Repository.GenericRepository;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class PersonnelRepository : GenericRepository<PersonnelEntity>, IPersonnelRepository
    {
        AppDbContext _appDbContext;
        public PersonnelRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<PersonnelEntity> GetAll()
        {
            return _context.Set<PersonnelEntity>().Where(x => x.IsDeleted == false);
        }
        public override IQueryable<PersonnelEntity> Where(Expression<Func<PersonnelEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
