using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.BaseService;
using Moor.Model.Models.MoorModels.AgencyModel;
using Moor.Model.Models.MoorModels.DriverModel;
using Moor.Model.Utilities;

namespace Moor.Core.Services.MoorService
{
    public interface IAgencyService : IService<AgencyEntity>
    {
        public Task<DataResult> Save(AgencyModel agencyModel);
        //public Task<DataResult> Update(AgencyModel agencyModel);
    }
}
