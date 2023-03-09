using Moor.Core.Attributes;
using Moor.Core.Entities.Base;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    public class AgencyEntity : BaseEntity
    {
        [DataFilter]
        [Column("name")]
        public string Name { get; set; }

        [DataFilter]
        [Column("title")]
        public string Title { get; set; }

        [DataFilter]
        [Column("tax_office")]
        public string TaxOffice { get; set; }

        [DataFilter]
        [Column("tax_number")]
        public string TaxNumber { get; set; }

        [DataFilter]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [DataFilter]
        [Column("operation_phone_number")]
        public string OperationPhoneNumber { get; set; }

        [DataFilter]
        [Column("media_path")]
        public string MediaPath { get; set; }

        [DataFilter]
        [Column("email")]
        public string Email { get; set; }

        [DataFilter]
        [Column("details")]
        public string Details { get; set; }

        [DataFilter]
        [Column("reception_price")]
        public decimal ReceptionPrice { get; set; }

        [DataFilter]
        [Column("personnel_id")]
        public long? PersonnelId { get; set; } 

        [DataFilter]
        [Column("city_id")]
        public long? CityId { get; set; }

        [DataFilter]
        [Column("county_id")]
        public long? CountyId { get; set; }

        [DataFilter]
        [ForeignKey("PersonnelId")]
        public virtual PersonnelEntity Personnel { get; set; }

        [DataFilter]
        [ForeignKey("CityId")]
        public virtual CityEntity City { get; set; }

        [DataFilter]
        [ForeignKey("CountyId")]
        public virtual CountyEntity County { get; set; }
    }
}
