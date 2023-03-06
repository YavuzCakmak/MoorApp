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
    [Table("city")]
    public class CityEntity : BaseEntity
    {
        [DataFilter]
        [Column("country_id")]
        public long CountryId { get; set; }

        [DataFilter]
        [Column("code")]
        public string Code { get; set; }

        [DataFilter]
        [Column("name")]
        public string Name { get; set; }

        [DataFilter]
        [ForeignKey("CountryId")]
        public virtual CountryEntity Country { get; set; }
    }
}
