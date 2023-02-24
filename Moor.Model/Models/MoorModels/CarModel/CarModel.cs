using Moor.Model.Models.Base;
using Moor.Model.Models.MoorModels.CarParameterModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Model.Models.MoorModels.CarModel
{
    public class CarModel : BaseModel
    {
        public long CarParameterId { get; set; }
        public string NumberPlate { get; set; }
        public CarParameterModel.CarParameterModel CarParameter { get; set; }
    }
}
