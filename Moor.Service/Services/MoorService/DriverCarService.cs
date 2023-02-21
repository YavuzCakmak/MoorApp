using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class DriverCarService : Service<DriverCarEntity>, IDriverCarService
    {
        private readonly IDriverCarRepository _driverCarRepository;
        private readonly IMapper _mapper;

        public DriverCarService(IGenericRepository<DriverCarEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IDriverCarRepository driverCarRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _driverCarRepository = driverCarRepository;
        }
    }
}
