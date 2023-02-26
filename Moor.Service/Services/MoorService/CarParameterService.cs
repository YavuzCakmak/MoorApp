using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;
using Moor.Model.Dtos.MoorDto.CarParameterDto;
using Moor.Core.Extension.String;
using Moor.Core.SSH.Concretion;

namespace Moor.Service.Services.MoorService
{
    public class CarParameterService : Service<CarParameterEntity>, ICarParameterService
    {
        private readonly ICarParameterRepository _carParameterRepository;
        private readonly IMapper _mapper;

        public CarParameterService(IGenericRepository<CarParameterEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, ICarParameterRepository carParameterRepository) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _carParameterRepository = carParameterRepository;
        }

        public async Task<CarParameterDto> Save(CarParameterDto carParameterDto)
        {
           var carParameterEntity =  await base.AddAsync(_mapper.Map<CarParameterEntity>(carParameterDto));
           return _mapper.Map<CarParameterDto>(carParameterEntity);
        }
    }
}
