using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class CountyService : Service<CountyEntity>, ICountyService
    {
        private readonly ICountyRepository _countyRepository;
        private readonly IMapper _mapper;

        public CountyService(IGenericRepository<CountyEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, ICountyRepository countyRepository) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _countyRepository = countyRepository;
        }
    }
}
