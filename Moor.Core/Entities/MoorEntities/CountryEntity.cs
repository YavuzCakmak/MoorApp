using Moor.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("country")]
    public class CountryEntity : BaseEntity
    {
        [Column("a2_code")]
        public string A2Code { get; set; }

        [Column("a3_code")]
        public string A3Code { get; set; }

        [Column("num_code")]
        public string NumCode { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
