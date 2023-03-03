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
    public class TransferRepository : GenericRepository<TransferEntity>, ITransferRepository
    {
        AppDbContext _appDbContext;
        public TransferRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }
        public override IQueryable<TransferEntity> GetAll()
        {
            return _context.Set<TransferEntity>().Where(x => x.IsDeleted == false);
        }
        public override IQueryable<TransferEntity> Where(Expression<Func<TransferEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
