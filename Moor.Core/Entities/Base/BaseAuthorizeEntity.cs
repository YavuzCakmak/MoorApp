using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Moor.Core.Entities.Base
{
    public class BaseAuthorizeEntity
    {
        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
