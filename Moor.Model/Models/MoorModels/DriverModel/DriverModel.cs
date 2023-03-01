using Moor.Model.Model.Authorize;
using Moor.Model.Models.Base;

namespace Moor.Model.Models.MoorModels.DriverModel
{
    public class DriverModel : BaseModel
    {
        public long StaffId { get; set; }
        public decimal Price { get; set; }
        public virtual PersonnelModel Personnel { get; set; }
    }
}
