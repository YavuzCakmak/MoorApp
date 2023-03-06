using Moor.Core.Attributes;
using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("car_parameter")]
    public class CarParameterEntity : BaseEntity
    {
        [DataFilter]
        [Column("brand")]
        public string Brand { get; set; }

        [DataFilter]
        [Column("model")]
        public string Model { get; set; }

        [DataFilter]
        [Column("media_path")]
        public string MediaPath { get; set; }
    }
}
