using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Entities.MoorEntities
{
    public class CountryEntity
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("a2_code")]
        public string A2Code { get; set; }

        [Column("a3_code")]
        public string A3Code { get; set; }

        [Column("num_code")]
        public string NumCode { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
