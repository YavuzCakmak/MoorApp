using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Repository.Repositories
{
    public class CarParameterRepository : GenericRepository<CarParameterEntity>, ICarParameterRepository
    {
        public CarParameterRepository(AppDbContext context) : base(context)
        {
        }
    }
}
