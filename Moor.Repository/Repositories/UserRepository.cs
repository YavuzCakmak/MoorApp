using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository.GenericRepository;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class UserRepository : GenericRepository<UserEntity> , IUserRepository
    {
        AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }
        public override IQueryable<UserEntity> GetAll()
        {
            return _context.Set<UserEntity>().Where(x => x.IsDeleted == false);
        }
        public override IQueryable<UserEntity> Where(Expression<Func<UserEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
