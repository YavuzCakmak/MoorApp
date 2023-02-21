using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class CityService : Service<CityEntity>, ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(IGenericRepository<CityEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, ICityRepository cityRepository) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
        }
    }
}
