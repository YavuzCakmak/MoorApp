using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.BaseService;
using Moor.Model.Dtos.MoorDto.CarDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Services.MoorService
{
    public interface ICarService : IService<CarEntity>
    {
        public Task<CarDto> Save(CarDto carDto);
    }
}
