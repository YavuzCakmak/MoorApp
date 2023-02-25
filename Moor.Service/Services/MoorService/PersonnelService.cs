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
    public class PersonnelService : Service<PersonnelEntity>, IPersonnelService
    {
        private readonly IPersonnelRepository _personnelRepository;
        private readonly IMapper _mapper;

        public PersonnelService(IGenericRepository<PersonnelEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IPersonnelRepository personnelRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _personnelRepository = personnelRepository;
        }
    }
}
