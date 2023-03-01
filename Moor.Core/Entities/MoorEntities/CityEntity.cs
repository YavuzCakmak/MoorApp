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
        [Column("country_id")]
        public long CountryId { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [ForeignKey("CountryId")]
        public virtual CountryEntity Country { get; set; }
    }
}
