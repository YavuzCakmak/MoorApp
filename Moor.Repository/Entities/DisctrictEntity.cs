using Moor.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Repository.Entities
{
    public class DisctrictEntity : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
