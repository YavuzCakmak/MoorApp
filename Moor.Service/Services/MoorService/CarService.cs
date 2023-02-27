using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Extension.String;
using Moor.Core.Repositories;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Models.MoorModels.CarParameterModel;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class CarService : Service<CarEntity>, ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarParameterService _carParameterService;
        private readonly IMapper _mapper;

        public CarService(IGenericRepository<CarEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, ICarRepository carRepository, ICarParameterService carParameterService) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _carRepository = carRepository;
            _carParameterService = carParameterService;
        }

        public async Task<CarDto> Save(CarDto carDto)
        {
            var carNewModel = await base.AddAsync(_mapper.Map<CarEntity>(carDto));
            return _mapper.Map<CarDto>(carNewModel);
        }
        public override Task UpdateAsync(CarEntity entity)
        {
            return base.UpdateAsync(entity);
        }

    }
}
