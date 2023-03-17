using System.ComponentModel.DataAnnotations.Schema;
using Moor.Core.Attributes;
using Moor.Core.Entities.Base;

namespace Moor.Core.Entities.MoorEntities.AuthorizeEntities
{
    public class PersonnelEntity : BaseEntity
    {
        [DataFilter]
        [Column("username")]
        public string UserName { get; set; }

        [DataFilter]
        [Column("password")]
        public string Password { get; set; }

        [DataFilter]
        [Column("status")]
        public int Status { get; set; } = 0;

        [DataFilter]
        [Column("first_name")]
        public string? FirstName { get; set; }

        [DataFilter]
        [Column("last_name")]
        public string? LastName { get; set; }
        
        [DataFilter]
        [Column("media_path")]
        public string? MediaPath { get; set; }

        [DataFilter]
        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [DataFilter]
        [Column("email")]
        public string? Email { get; set; }

        [DataFilter]
        [Column("number_of_incorrect_entries")]
        public int NumberOfIncorrectEntries { get; set; } = 0;

        [DataFilter]
        [Column("password_change_date")]
        public DateTime PasswordChangeDate { get; set; } = DateTime.Now;
    }
}
