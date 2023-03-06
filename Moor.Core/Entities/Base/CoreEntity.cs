
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Moor.Core.Attributes;

namespace Moor.Core.Entities.Base
{
    public abstract class CoreEntity
    {
        [Key]
        [DataFilter]
        [Column("id")]
        public long Id { get; set; }

        [DataFilter]
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
