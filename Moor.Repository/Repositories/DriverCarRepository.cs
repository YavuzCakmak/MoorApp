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
    public class DriverCarRepository : GenericRepository<DriverCarEntity>, IDriverCarRepository
    {
        AppDbContext _appDbContext;
        public DriverCarRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }
        public override IQueryable<DriverCarEntity> GetAll()
        {
            return _context.Set<DriverCarEntity>().Where(x => x.IsDeleted == false).Include(x=> x.Driver).Include(x=> x.Car);
        }
        public override IQueryable<DriverCarEntity> Where(Expression<Func<DriverCarEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x=> x.Driver).Include(x=> x.Car);
        }
    }
}
