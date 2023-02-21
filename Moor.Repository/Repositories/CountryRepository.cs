using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository.GenericRepository;
using System.Linq.Expressions;

namespace Moor.Repository.Repositories
{
    public class CountryRepository : GenericRepository<CountryEntity>, ICountryRepository
    {
        AppDbContext _appDbContext;
        public CountryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<CountryEntity> GetAll()
        {
            return _context.Set<CountryEntity>().Where(x => x.IsDeleted == false);
        }
        public override IQueryable<CountryEntity> Where(Expression<Func<CountryEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
