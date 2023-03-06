using Moor.Core.Attributes;
using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("car")]
    public class CarEntity : BaseEntity
    {
        [DataFilter]
        [Column("car_parameter_id")]
        public long CarParameterId { get; set; }

        [DataFilter]
        [Column("number_plate")]
        public string NumberPlate { get; set; }

        [DataFilter]
        [ForeignKey("CarParameterId")]
        public CarParameterEntity CarParameter { get; set; }
    }
}
