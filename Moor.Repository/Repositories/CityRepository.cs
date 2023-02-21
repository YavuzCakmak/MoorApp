using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Repository.Repositories
{
    public class CityRepository : GenericRepository<CityEntity>, ICityRepository
    {
        AppDbContext _appDbContext;
        public CityRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<CityEntity> GetAll()
        {
            return _context.Set<CityEntity>().Where(x => x.IsDeleted == false).Include(x => x.Country);
        }
        public override IQueryable<CityEntity> Where(Expression<Func<CityEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.Country);
        }
    }
}
