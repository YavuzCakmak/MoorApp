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
    public class PriceService : Service<PriceEntity>, IPriceService
    {
        private readonly IPriceRepository _priceRepository;
        private readonly IMapper _mapper;

        public PriceService(IGenericRepository<PriceEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IPriceRepository priceRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _priceRepository = priceRepository;
        }
    }
}
