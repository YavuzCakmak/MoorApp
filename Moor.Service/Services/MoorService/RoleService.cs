using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Core.Repositories.MoorRepository.AuthorizeRepository;

namespace Moor.Service.Services.MoorService
{
    public class RoleService : Service<RoleEntity>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IGenericRepository<RoleEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, IRoleRepository roleRepository) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }
    }
}
