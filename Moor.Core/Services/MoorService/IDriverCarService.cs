using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.BaseService;
using Moor.Model.Models.MoorModels.CarParameterModel;
using Moor.Model.Models.MoorModels.DriverCarModel;
using Moor.Model.Models.MoorModels.DriverModel;
using Moor.Model.Utilities;

namespace Moor.Core.Services.MoorService
{
    public interface IDriverCarService : IService<DriverCarEntity>
    {
        public Task<DataResult> Save(DriverModel driverModel);
    }
}
