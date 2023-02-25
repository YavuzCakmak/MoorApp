using Moor.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Entities.MoorEntities.AuthorizeEntities
{
    [Table("privilege")]
    public class PrivilegeEntity : BaseAuthorizeEntity
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
