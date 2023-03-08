using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;
using Moor.Model.Utilities;
using Moor.Model.Models.MoorModels.DriverModel;
using Moor.Core.Extension.String;
using Moor.Core.Enums;

namespace Moor.Service.Services.MoorService
{
    public class DriverService : Service<DriverEntity>, IDriverService
    {
        private readonly IPersonnelService _personnelService;
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public DriverService(IGenericRepository<DriverEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IDriverRepository driverRepository, IPersonnelService personnelService) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
            _personnelService = personnelService;
        }

        public async Task<DataResult> Save(DriverModel driverModel)
        {
            DataResult dataResult = new DataResult();
            var personnelModel = await _personnelService.GetByIdAsync(driverModel.PersonnelId);

            if (personnelModel.IsNotNull())
            {
                DriverEntity driverEntity = new DriverEntity();
                driverEntity.PersonnelId = driverModel.PersonnelId;
                driverEntity.Status = Convert.ToInt32(Status.AKTIF);
                driverEntity.Price = driverModel.Price;
                driverEntity.IsDeleted = false;
                var driverAddResult = await base.AddAsync(driverEntity);
                if (driverAddResult.IsNotNull())
                {
                    dataResult.IsSuccess = true;
                    dataResult.PkId = driverAddResult.Id;
                    return dataResult;
                }
                else
                {
                    dataResult.IsSuccess = false;
                    return dataResult;
                }
            }
            else
            {
                dataResult.IsSuccess = false;
                dataResult.ErrorMessage = "Şöföre ait kişi bilgisi bulunamadı";
                return dataResult;
            }
        }
    }
}
