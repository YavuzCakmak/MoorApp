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
    public class PersonnelRoleService : Service<PersonnelRoleEntity>, IPersonnelRoleService
    {
        private readonly IPersonnelRoleRepository _personnelRoleRepository;
        private readonly IMapper _mapper;

        public PersonnelRoleService(IGenericRepository<PersonnelRoleEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, IPersonnelRoleRepository personnelRoleRepository) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _personnelRoleRepository = personnelRoleRepository;
        }
    }
}
