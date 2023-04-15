using Moor.Core.Attributes;
using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    public class TravellerEntity : BaseEntity
    {
        [DataFilter]
        [Column("transfer_id")]
        public long? TransferId { get; set; }

        [DataFilter]
        [Column("first_name")]
        public string? FirstName { get; set; }

        [DataFilter]
        [Column("last_name")]
        public string? LastName { get; set; }

        [DataFilter]
        [Column("email")]
        public string? Email { get; set; }

        [DataFilter]
        [Column("phone_number")]
        public string? PhoneNumber { get; set; }
    }
}
