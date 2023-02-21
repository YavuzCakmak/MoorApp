using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class StaffService : Service<StaffEntity>, IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public StaffService(IGenericRepository<StaffEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IStaffRepository staffRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _staffRepository = staffRepository;
        }
    }
}
