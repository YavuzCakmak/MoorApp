﻿using Moor.Model.Models.Base;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Model.Models.MoorModels.DriverModel;

namespace Moor.Model.Models.MoorModels.DriverCarModel
{
    public class DriverCarModel : BaseModel
    {
        public long DriverId { get; set; }
        public long CarId { get; set; }
        public virtual CarModel.CarModel Car { get; set; }
        public virtual DriverModel.DriverModel Driver { get; set; }
    }
}
