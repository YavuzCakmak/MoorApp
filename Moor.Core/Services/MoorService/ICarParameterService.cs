using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.BaseService;
using Moor.Model.Dtos.MoorDto.CarParameterDto;

namespace Moor.Core.Services.MoorService
{
    public interface ICarParameterService : IService<CarParameterEntity>
    {
        public Task<CarParameterDto> Save(CarParameterDto carParameterDto);
    }
}
