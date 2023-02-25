using Moor.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Entities.MoorEntities.AuthorizeEntities
{
    [Table("role_privilege")]
    public class RolePrivilegeEntity : BaseAuthorizeEntity
    {
        [Column("role_id")]

        public long RoleId { get; set; }

        [Column("privilege_id")]

        public long PrivilegeId { get; set; }

        [ForeignKey("RoleId")]
        public RoleEntity Role { get; set; }

        [ForeignKey("PrivilegeId")]
        public PrivilegeEntity Privilege { get; set; }
    }
}
