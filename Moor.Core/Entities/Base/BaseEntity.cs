using Moor.Core.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.Base
{
    public abstract class BaseEntity : CoreEntity
    {
        [DataFilter]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [DataFilter]
        [Column("update_date")]
        public DateTime? UpdateDate { get; set; }

        [DataFilter]
        [Column("status")]
        public int Status { get; set; }
    }
}
