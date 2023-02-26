using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Extension.String;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository;
using Moor.Repository.GenericRepository;
using Moor.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Repository.Repositories
{
    public class CarParameterRepository : GenericRepository<CarParameterEntity>, ICarParameterRepository
    {
        AppDbContext _appDbContext;
        public CarParameterRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = (AppDbContext)appDbContext;
        }

        public override IQueryable<CarParameterEntity> GetAll()
        {
            return _context.Set<CarParameterEntity>().Where(x => x.IsDeleted == false);
        }
        public override IQueryable<CarParameterEntity> Where(Expression<Func<CarParameterEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
