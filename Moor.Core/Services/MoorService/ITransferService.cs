using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.BaseService;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferPostDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto;

namespace Moor.Core.Services.MoorService
{
    public interface ITransferService : IService<TransferEntity>
    {
        public Task<TransferViewDto> CreateTransfer(TransferPostDto transferPostDto);
        public Task<List<TransferViewDto>> MapTransferViewDtos(List<TransferEntity> transferEntities);
        public Task<TransferViewDto> MapTransferViewDto(TransferEntity transferEntity);
    }
}
