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

namespace Moor.Service.Services.MoorService
{
    public class CountryService : Service<CountryEntity>, ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(IGenericRepository<CountryEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, ICountryRepository countryRepository) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }
    }
}
