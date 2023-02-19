using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class CarParameterService : Service<CarParameterEntity>, ICarParameterService
    {
        private readonly ICarParameterRepository _carParameterRepository;
        private readonly IMapper _mapper;

        public CarParameterService(IGenericRepository<CarParameterEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, ICarParameterRepository _carParameterRepository) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _carParameterRepository = _carParameterRepository;
        }
    }
}
