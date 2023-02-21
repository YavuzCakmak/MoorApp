using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;

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
    }
}
