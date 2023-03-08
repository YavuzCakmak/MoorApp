using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.BaseService;
using Moor.Model.Dtos.MoorDto.CarParameterDto;
using Moor.Model.Models.MoorModels.CarParameterModel;
using Moor.Model.Utilities;

namespace Moor.Core.Services.MoorService
{
    public interface ICarParameterService : IService<CarParameterEntity>
    {
        public Task<DataResult> Save(CarParameterModel carParameterDto);
    }
}
