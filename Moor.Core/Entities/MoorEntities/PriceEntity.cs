using Moor.Core.Attributes;
using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    public class PriceEntity : BaseEntity
    {
        [DataFilter]
        [Column("car_parameter_id")]
        public long CarParameterId { get; set; }

        [DataFilter]
        [Column("district_id")]
        public long DistrictId { get; set; }

        [DataFilter]
        [Column("Price")]
        public decimal Price { get; set; }

        [DataFilter]
        [ForeignKey("DistrictId")]
        public virtual DistrictEntity District { get; set; }

        [DataFilter]
        [ForeignKey("CarParameterId")]
        public virtual CarParameterEntity CarParameter { get; set; }
    }
}
