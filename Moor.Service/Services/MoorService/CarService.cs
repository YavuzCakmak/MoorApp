using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Enums;
using Moor.Core.Extension.String;
using Moor.Core.Repositories;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Models.MoorModels.Car;
using Moor.Model.Models.MoorModels.CarParameterModel;
using Moor.Model.Utilities;
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

        public async Task<DataResult> Save(CarPostModel carPostModel)
        {
            #region Objects
            DataResult dataResult = new DataResult();
            CarEntity carEntity = new CarEntity();
            #endregion

            var carParameterModel = await _carParameterService.GetByIdAsync(carPostModel.CarParameterId);
            if (carParameterModel.IsNotNull())
            {
                carEntity.CarParameterId = carPostModel.CarParameterId;
                carEntity.NumberPlate = carPostModel.NumberPlate;
                carEntity.Status = Convert.ToInt32(Status.AKTIF);
                carEntity.IsDeleted = false;
                var carEntityResult = await base.AddAsync(carEntity);
                if (carEntityResult.IsNotNull() && carEntityResult.Id.IsNotNull())
                {
                    dataResult.PkId = carEntity.Id;
                    dataResult.IsSuccess = true;
                    return dataResult;
                }
                else
                {
                    dataResult.IsSuccess = false;
                    dataResult.ErrorMessage = "Kayıt esnasında hata oluştu.";
                    return dataResult;
                }
            }
            else
            {
                dataResult.IsSuccess = false;
                dataResult.ErrorMessage = "Araç bilgisi bulunamadı.";
                return dataResult;
            }
        }

        public async Task<DataResult> Update(CarPostModel carPostModel)
        {
            #region Objects
            DataResult dataResult = new DataResult();
            CarEntity carEntity = new CarEntity();
            #endregion

            carEntity = base.Where(x => x.Id == carPostModel.Id).FirstOrDefault();
            if (carEntity.IsNotNull())
            {
                if (carPostModel.CarParameterId.IsNotNull() && carPostModel.CarParameterId > 0)
                {
                    var carParameterModel = await _carParameterService.GetByIdAsync(carPostModel.CarParameterId);
                    if (carParameterModel.IsNotNull())
                    {
                        carEntity.CarParameterId = carPostModel.CarParameterId;
                        carEntity.NumberPlate = carPostModel.NumberPlate;
                        await base.UpdateAsync(carEntity);
                        dataResult.IsSuccess = true;
                        return dataResult;
                    }
                    else
                    {
                        dataResult.IsSuccess = false;
                        dataResult.ErrorMessage = "Araç bilgisi bulunamadı.";
                        return dataResult;
                    }
                }
                else
                {
                    carEntity.NumberPlate = carPostModel.NumberPlate;
                    await base.UpdateAsync(carEntity);
                    dataResult.IsSuccess = true;
                    return dataResult;
                }
            }
            else
            {
                dataResult.IsSuccess = false;
                dataResult.ErrorMessage = "Araç bulunamadı.";
                return dataResult;
            }
        }
    }
}
