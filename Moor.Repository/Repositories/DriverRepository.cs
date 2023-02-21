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
    public class DriverRepository : GenericRepository<DriverEntity>, IDriverRepository
    {
        AppDbContext _appDbContext;
        public DriverRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<DriverEntity> GetAll()
        {
            return _context.Set<DriverEntity>().Where(x => x.IsDeleted == false).Include(x => x.Staff);
        }
        public override IQueryable<DriverEntity> Where(Expression<Func<DriverEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.Staff);
        }
    }
}
