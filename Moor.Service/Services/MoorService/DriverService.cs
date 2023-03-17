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
using Moor.Model.Dtos.MoorDto.DriverDto;
using Moor.Model.Utilities.Authentication;
using Moor.Service.Utilities.AuthorizeHelpers;
using Moor.Model.Models.MoorModels.DriverModel.DriverWalletModel;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Model.Models.MoorModels.AgencyModel;

namespace Moor.Service.Services.MoorService
{
    public class DriverService : Service<DriverEntity>, IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public DriverService(IGenericRepository<DriverEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IDriverRepository driverRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
        }
        public async Task<DataResult> Update(DriverDto driverDto)
        {
            DataResult dataResult = new DataResult();
            var driverEntity = base.Where(x => x.Id == driverDto.Id).FirstOrDefault();
            if (driverEntity.IsNotNull())
            {
                driverEntity.Price = driverDto.Price;
                await base.UpdateAsync(driverEntity);
                dataResult.IsSuccess = true;
                return dataResult;
            }
            else
            {
                dataResult.IsSuccess = false;
                dataResult.ErrorMessage = "Güncellenecek kişi bulunamadı";
                return dataResult;
            }
        }
    }
}
