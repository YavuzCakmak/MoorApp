using Moor.Model.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.PriceModel
{
    public class PriceModel : BaseModel
    {
        public long CarParameterId { get; set; }
        public long DistrictId { get; set; }
        public decimal Price { get; set; }
        public virtual DistrictModel.DistrictModel District { get; set; }
        public virtual CarParameterModel.CarParameterModel CarParameter { get; set; }
    }
}
