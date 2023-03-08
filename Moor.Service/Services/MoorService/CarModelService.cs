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
    public class CarModelService : Service<CarModelEntity>, ICarModelService
    {
        private readonly ICarModelRepository _carModelRepository;
        private readonly IMapper _mapper;

        public CarModelService(IGenericRepository<CarModelEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, ICarModelRepository carModelRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _carModelRepository = carModelRepository;
        }
    }
}
