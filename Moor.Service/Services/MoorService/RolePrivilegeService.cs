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
    public class RolePrivilegeService : Service<RolePrivilegeEntity>, IRolePrivilegeService
    {
        private readonly IRolePrivilegeRepository _rolePrivilegeRepository;
        private readonly IMapper _mapper;

        public RolePrivilegeService(IGenericRepository<RolePrivilegeEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IRolePrivilegeRepository rolePrivilegeRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _rolePrivilegeRepository = rolePrivilegeRepository;
        }
    }
}
