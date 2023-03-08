using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Service.Services.MoorService
{
    public class CarBrandService : Service<CarBrandEntity>, ICarBrandService
    {
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly IMapper _mapper;

        public CarBrandService(IGenericRepository<CarBrandEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, ICarBrandRepository carBrandRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _carBrandRepository = carBrandRepository;
        }
    }
}
