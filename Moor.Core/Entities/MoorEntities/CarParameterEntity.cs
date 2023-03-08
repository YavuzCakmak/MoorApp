using Moor.Core.Attributes;
using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("car_parameter")]
    public class CarParameterEntity : BaseEntity
    {
        [DataFilter]
        [Column("car_brand_id")]
        public long CarBrandId { get; set; }

        [DataFilter]
        [Column("car_model_id")]
        public long CarModelId { get; set; }

        [DataFilter]
        [ForeignKey("CarBrandId")]
        public virtual CarBrandEntity CarBrand { get; set; }

        [DataFilter]
        [ForeignKey("CarModelId")]
        public virtual CarModelEntity CarModel { get; set; }
    }
}
