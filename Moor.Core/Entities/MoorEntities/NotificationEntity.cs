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
    [Table("notification")]
    public class NotificationEntity : BaseEntity
    {
        [DataFilter]
        [Column("agency_id")]
        public long? AgencyId { get; set; }

        [DataFilter]
        [Column("transfer_id")]
        public long? TransferId { get; set; }

        [DataFilter]
        [Column("explanation")]
        public string? Explanation { get; set; }

        [DataFilter]
        [Column("is_read")]
        public bool IsRead { get; set; }

        [DataFilter]
        [Column("is_first")]
        public bool IsFirst { get; set; }

        [DataFilter]
        [ForeignKey("TransferId")]
        public virtual TransferEntity Transfer { get; set; }

        [DataFilter]
        [ForeignKey("AgencyId")]
        public virtual AgencyEntity Agency { get; set; }
    }
}
