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
    [Table("wallet")]
    public class WalletEntity : BaseEntity
    {
        [DataFilter]
        [Column("agency_id")]
        public long? AgencyId { get; set; }

        [DataFilter]
        [Column("driver_id")]
        public long? DriverId { get; set; }

        [DataFilter]
        [Column("total_amount")]
        public decimal? TotalAmount { get; set; }

        [DataFilter]
        [ForeignKey("AgencyId")]
        public virtual AgencyEntity? Agency { get; set; }

        [DataFilter]
        [ForeignKey("DriverId")]
        public virtual DriverEntity? Driver { get; set; }
    }
}
