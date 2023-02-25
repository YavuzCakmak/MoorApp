using Moor.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Entities.MoorEntities.AuthorizeEntities
{
    [Table("personnel_role")]
    public class PersonnelRoleEntity : BaseAuthorizeEntity
    {

        [Column("personnel_id")]
        public long PersonnelId { get; set; }


        [Column("role_id")]
        public long RoleId { get; set; }

        [ForeignKey("PersonnelId")]
        public virtual PersonnelEntity Personnel { get; set; }

        [ForeignKey("RoleId")]
        public virtual RoleEntity Role { get; set; }
    }
}
