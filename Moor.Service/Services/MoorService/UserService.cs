using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class UserService : Service<UserEntity>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<UserEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
    }
}
