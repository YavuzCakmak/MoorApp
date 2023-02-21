using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("district")]
    public class DistrictEntity : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
