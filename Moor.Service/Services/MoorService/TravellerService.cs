using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;
using Moor.Repository.Repositories;

namespace Moor.Service.Services.MoorService
{
    public class TravellerService : Service<TravellerEntity>, ITravellerService
    {
        private readonly ITravellerRepository _travellerRepository;
        private readonly IMapper _mapper;

        public TravellerService(IGenericRepository<TravellerEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, ITravellerRepository travellerRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _travellerRepository = travellerRepository;
        }
    }
}
