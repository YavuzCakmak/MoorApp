using Moor.Model.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.Car
{
    public class CarPostModel : BaseModel
    {
        public long CarParameterId { get; set; }
        public string NumberPlate { get; set; }
    }
}
