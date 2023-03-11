using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;
using Moor.Model.Utilities;
using Moor.Model.Models.MoorModels.AgencyModel;
using Moor.Core.Extension.String;

namespace Moor.Service.Services.MoorService
{
    public class AgencyService : Service<AgencyEntity>, IAgencyService
    {
        private readonly IAgencyRepository _agencyRepository;
        private readonly IMapper _mapper;

        public AgencyService(IGenericRepository<AgencyEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, IAgencyRepository agencyRepository) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
        }

        public async Task<DataResult> Save(AgencyModel agencyModel)
        {
            #region Object
            DataResult dataResult = new DataResult();
            #endregion

            var agencyEntity = _mapper.Map<AgencyEntity>(agencyModel);
            var agencyResult = await base.AddAsync(agencyEntity);
            if (agencyResult.Id.IsNotNull() && agencyResult.Id > 0)
            {
                dataResult.PkId = agencyResult.Id;
                dataResult.IsSuccess = true;
                return dataResult;
            }
            else
            {
                dataResult.ErrorMessage = "Acente kayıt edilirken hata oluştu.";
                dataResult.IsSuccess = false;
                return dataResult;
            }
        }
    }
}
