using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;
using Moor.Model.Utilities;
using Moor.Model.Models.MoorModels.DriverCarModel;
using Moor.Core.Extension.String;
using Moor.Core.Enums;

namespace Moor.Service.Services.MoorService
{
    public class DriverCarService : Service<DriverCarEntity>, IDriverCarService
    {
        private readonly ICarService _carService;
        private readonly IDriverService _driverService;
        private readonly IDriverCarRepository _driverCarRepository;
        private readonly IMapper _mapper;

        public DriverCarService(IGenericRepository<DriverCarEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IDriverCarRepository driverCarRepository, ICarService carService, IDriverService driverService) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _driverCarRepository = driverCarRepository;
            _carService = carService;
            _driverService = driverService;
        }

        public async Task<DataResult> Save(DriverCarModel driverCarModel)
        {
            #region
            DataResult dataResult = new DataResult();
            DriverCarEntity driverCarEntity = new DriverCarEntity();
            #endregion

            var carModel = await _carService.GetByIdAsync(driverCarModel.CarId);
            if (driverCarModel.IsNotNull())
            {
                var driverModel = await _driverService.GetByIdAsync(driverCarModel.DriverId);
                if (driverModel.IsNotNull())
                {
                    driverCarEntity.Status = Convert.ToInt32(Status.AKTIF);
                    driverCarEntity.IsDeleted = false;
                    driverCarEntity.DriverId = driverCarModel.DriverId;
                    driverCarEntity.CarId = driverCarModel.CarId;
                    var driverCarResult = await base.AddAsync(driverCarEntity);
                    if (driverCarResult.IsNotNull() && driverCarResult.Id.IsNotNull())
                    {
                        dataResult.PkId = driverCarResult.Id;
                        dataResult.IsSuccess = true;
                        return dataResult;
                    }
                }
                else
                {
                    dataResult.IsSuccess = false;
                    dataResult.ErrorMessage = "Araç bilgisi Bulunamadı.";
                    return dataResult;
                }
            }
            else
            {
                dataResult.IsSuccess = false;
                dataResult.ErrorMessage = "Araç bilgisi Bulunamadı.";
                return dataResult;
            }
            return dataResult;
        }
    }
}
