using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class WalletService : Service<WalletEntity>, IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;

        public WalletService(IGenericRepository<WalletEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IWalletRepository walletRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _walletRepository = walletRepository;
        }
    }
}
