using Moor.Core.Attributes;
using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities.AuthorizeEntities
{
    [Table("role")]
    public class RoleEntity : BaseEntity
    {
        [DataFilter]
        [Column("name")]
        public string Name { get; set; }
    }
}
