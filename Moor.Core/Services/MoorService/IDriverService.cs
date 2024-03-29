﻿using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories;
using Moor.Core.Services.BaseService;
using Moor.Model.Dtos.MoorDto.DriverDto;
using Moor.Model.Models.MoorModels.CarParameterModel;
using Moor.Model.Models.MoorModels.DriverModel;
using Moor.Model.Models.MoorModels.DriverModel.DriverWalletModel;
using Moor.Model.Utilities;

namespace Moor.Core.Services.MoorService
{
    public interface IDriverService : IService<DriverEntity>
    {
        //public Task<DataResult> Save(DriverModel driverModel);
        public Task<DataResult> Update(DriverDto driverDto);
    }
}
