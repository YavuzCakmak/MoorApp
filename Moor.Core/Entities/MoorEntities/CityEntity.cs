using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Entities.MoorEntities
{
    public class CityEntity
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("country_id")]
        public long CountryId { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [ForeignKey("CountryId")]
        public virtual CountryEntity CountryEntity { get; set; }
    }
}
