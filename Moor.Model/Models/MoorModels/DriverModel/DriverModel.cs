using Moor.Model.Model.Authorize;
using Moor.Model.Models.Base;

namespace Moor.Model.Models.MoorModels.DriverModel
{
    public class DriverModel : BaseModel
    {
        #region CarObject
        public long CarParameterId { get; set; }
        public string Plate { get; set; }
        #endregion
        
        public PersonnelModel PersonnelModel{ get; set; }
        public decimal Price { get; set; }
    }
}
