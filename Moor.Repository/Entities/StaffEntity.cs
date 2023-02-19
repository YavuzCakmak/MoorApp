using Moor.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Repository.Entities
{
    public class StaffEntity : BaseEntity
    {
        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [Column("media_path")]
        public string? MediaPath { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserEntity UserEntity { get; set; }
    }
}
