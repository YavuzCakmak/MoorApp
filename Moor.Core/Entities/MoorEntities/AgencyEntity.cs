using Moor.Core.Entities.Base;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    public class AgencyEntity : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("tax_office")]
        public string TaxOffice { get; set; }
        [Column("tax_number")]
        public string TaxNumber { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("operation_phone_number")]
        public string OperationPhoneNumber { get; set; }
        [Column("media_path")]
        public string MediaPath { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("details")]
        public string Details { get; set; }
        [Column("reception_price")]
        public decimal ReceptionPrice { get; set; }
        [Column("personnel_id")]
        public long? PersonnelId { get; set; } // PersonnelId olacak 
        [Column("city_id")]
        public long? CityId { get; set; }
        [Column("county_id")]
        public long? CountyId { get; set; }

        [ForeignKey("PersonnelId")]
        public virtual PersonnelEntity Personnel { get; set; }
        [ForeignKey("CityId")]
        public virtual CityEntity City { get; set; }
        [ForeignKey("CountyId")]
        public virtual CountyEntity County { get; set; }
    }
}
