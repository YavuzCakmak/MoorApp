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
    public class PaymentDescriptionService : Service<PaymentDescriptionEntity>, IPaymentDescriptionService
    {
        private readonly IPaymentDescriptionRepository _paymentDescriptionRepository;
        private readonly IMapper _mapper;

        public PaymentDescriptionService(IGenericRepository<PaymentDescriptionEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IPaymentDescriptionRepository paymentDescriptionRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _paymentDescriptionRepository = paymentDescriptionRepository;
        }
    }
}
