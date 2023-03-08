using Moor.Model.Models.Base;

namespace Moor.Model.Models.MoorModels.CarParameterModel
{
    public class CarParameterModel : BaseModel
    {
        public long CarModelId { get; set; }
        public long CarBrandId { get; set; }
    }
}
