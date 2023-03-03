using Moor.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("driver_car")]
    public class DriverCarEntity : BaseEntity
    {
        [Column("driver_id")]
        public long? DriverId { get; set; }
        [Column("car_id")]
        public long CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual CarEntity Car { get; set; }

        [ForeignKey("DriverId")]
        public virtual DriverEntity Driver { get; set; }
    }
}
