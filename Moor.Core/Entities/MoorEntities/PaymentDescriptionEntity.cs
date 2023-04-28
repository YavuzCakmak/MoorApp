using Moor.Core.Attributes;
using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("payment_description")]
    public class PaymentDescriptionEntity : BaseEntity
    {
        [DataFilter]
        [Column("agency_id")]
        public long? AgencyId { get; set; }

        [DataFilter]
        [Column("driver_id")]
        public long? DriverId { get; set; }

        [DataFilter]
        [Column("description")]
        public string? Description { get; set; }

        [DataFilter]
        [Column("payment_date")]
        public DateTime? PaymentDate { get; set; }

        [DataFilter]
        [ForeignKey("AgencyId")]
        public virtual AgencyEntity? Agency { get; set; }

        [DataFilter]
        [ForeignKey("DriverId")]
        public virtual DriverEntity? Driver { get; set; }
    }
}
