using Moor.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Repository.Entities
{
    public class DriverEntity : BaseEntity
    {
        [Column("staff_id")]
        public long StaffId { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [ForeignKey("StaffId")]
        public virtual StaffEntity StaffEntity { get; set; }
    }
}
