using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class AgencyService : Service<AgencyEntity>, IAgencyService
    {
        private readonly IAgencyRepository _agencyRepository;
        private readonly IMapper _mapper;

        public AgencyService(IGenericRepository<AgencyEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, IAgencyRepository agencyRepository) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
        }
    }
}
