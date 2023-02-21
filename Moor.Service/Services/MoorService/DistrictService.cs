using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class DistrictService : Service<DistrictEntity>, IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IMapper _mapper;

        public DistrictService(IGenericRepository<DistrictEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, IDistrictRepository districtRepository) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _districtRepository = districtRepository;
        }
    }
}
