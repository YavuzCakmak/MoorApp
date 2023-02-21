using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class TransferService : Service<TransferEntity>, ITransferService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IMapper _mapper;

        public TransferService(IGenericRepository<TransferEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, ITransferRepository transferRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _transferRepository = transferRepository;
        }
    }
}
