using Moor.Core.Attributes;
using Moor.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("car_brand")]
    public class CarBrandEntity : BaseEntity
    {
        [DataFilter]
        [Column("brand")]
        public string Brand { get; set; }
    }
}
