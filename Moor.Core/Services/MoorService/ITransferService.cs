using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.BaseService;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferPostDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto;
using Moor.Model.Models.MoorModels.AgencyModel.AgencyWalletModel;
using Moor.Model.Models.MoorModels.AgencyModel.DebitForAgencyModel;
using Moor.Model.Models.MoorModels.DriverModel.DebitForDriverModel;
using Moor.Model.Models.MoorModels.DriverModel.DriverWalletModel;
using Moor.Model.Models.MoorModels.TransferModel.TransferChangeModel;
using Moor.Model.Utilities;

namespace Moor.Core.Services.MoorService
{
    public interface ITransferService : IService<TransferEntity>
    {
        public Task<DriverWalletModel> GetDriverWallet(long driverId);
        public Task<DataResult> AddDebitForDriver(DebitForDriverModel debitForDriverModel);
        public Task<DataResult> AddDebitForAgency(DebitForAgencyModel debitForAgencyModel);
        public Task<AgencyWalletModel> GetAgencyWallet(long agencyId);
        public Task<DataResult> CreateTransfer(TransferPostDto transferPostDto);
        public Task<DataResult> ChangeTransferStatus(TransferChangeModel transferChangeModel);
        public Task<List<TransferViewDto>> MapTransferViewDtos(List<TransferEntity> transferEntities);
        public Task<TransferViewDto> MapTransferViewDto(TransferEntity transferEntity);
    }
}
