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
using Moor.Model.Models.MoorModels.CarParameterModel;
using Moor.Model.Utilities;
using Moor.Core.Enums;

namespace Moor.Service.Services.MoorService
{
    public class CarParameterService : Service<CarParameterEntity>, ICarParameterService
    {
        private readonly ICarModelService _carModelService;
        private readonly ICarBrandService _carBrandService;
        private readonly ICarParameterRepository _carParameterRepository;
        private readonly IMapper _mapper;

        public CarParameterService(IGenericRepository<CarParameterEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, ICarParameterRepository carParameterRepository, ICarModelService carModelService, ICarBrandService carBrandService) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _carParameterRepository = carParameterRepository;
            _carModelService = carModelService;
            _carBrandService = carBrandService;
        }

        public async Task<DataResult> Save(CarParameterModel carParameterModel)
        {
            DataResult dataResult = new DataResult();
            CarParameterEntity carParameterEntity = new CarParameterEntity();

            var carModel = await _carModelService.GetByIdAsync(carParameterModel.CarModelId);
            if (carModel.IsNull())
            {
                dataResult.ErrorMessage = "Model mevcut değil";
                dataResult.IsSuccess = false;
                return dataResult;
            }
            var carBrand = await _carModelService.GetByIdAsync(carParameterModel.CarBrandId);
            if (carBrand.IsNull())
            {
                dataResult.ErrorMessage = "Model mevcut değil";
                dataResult.IsSuccess = false;
                return dataResult;
            }
            carParameterEntity.Status = Convert.ToInt32(Status.AKTIF);
            carParameterEntity.CarBrandId = carParameterModel.CarBrandId;
            carParameterEntity.CarModelId = carParameterModel.CarModelId;

            var carParameterAddResult = await base.AddAsync(carParameterEntity);
            if (carParameterAddResult.Id.IsNotNull())
            {
                dataResult.PkId = carParameterAddResult.Id;
                dataResult.IsSuccess = true;
                return dataResult;
            }
            return dataResult;
        }
    }
}
