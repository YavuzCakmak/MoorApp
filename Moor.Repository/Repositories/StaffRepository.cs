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
    public class StaffRepository : GenericRepository<StaffEntity>, IStaffRepository
    {
        AppDbContext _appDbContext;
        public StaffRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }
        public override IQueryable<StaffEntity> GetAll()
        {
            return _context.Set<StaffEntity>().Where(x => x.IsDeleted == false).Include(x => x.Personnel);
        }
        public override IQueryable<StaffEntity> Where(Expression<Func<StaffEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false).Include(x => x.Personnel);
        }
    }
}
