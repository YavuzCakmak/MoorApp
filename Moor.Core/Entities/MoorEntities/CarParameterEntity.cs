using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    public class CarParameterEntity : BaseEntity
    {
        [Column("brand")]
        public string Brand { get; set; }

        [Column("model")]
        public string Model { get; set; }

        [Column("media_path")]
        public string MediaPath { get; set; }
    }
}
