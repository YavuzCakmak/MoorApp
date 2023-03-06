using Moor.Core.Attributes;
using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("transfer")]
    public class TransferEntity : BaseEntity
    {
        [DataFilter]
        [Column("agency_id")]
        public long AgencyId { get; set; }

        [DataFilter]
        [Column("driver_id")]
        public long? DriverId { get; set; }

        [DataFilter]
        [Column("district_id")]
        public long DisctrictId { get; set; }

        [DataFilter]
        [Column("city_id")]
        public long CityId { get; set; }

        [DataFilter]
        [Column("county_id")]
        public long CountyId { get; set; }

        [DataFilter]
        [Column("car_parameter_id")]        
        public long CarParameterId { get; set; }

        [DataFilter]
        [Column("location")]
        public string Location { get; set; }

        [DataFilter]
        [Column("flight_code")]
        public string FlightCode { get; set; }

        [DataFilter]
        [Column("explanation")]
        public string Explanation { get; set; }

        [DataFilter]
        [Column("reception_type")]
        public int ReceptionType { get; set; }

        [DataFilter]
        [Column("direction_type")]
        public int DirectionType { get; set; }

        [DataFilter]
        [Column("departure_date")]
        public DateTime DepartureDate { get; set; }

        [DataFilter]
        [Column("return_date")]
        public DateTime ReturnDate { get; set; }

        [DataFilter]
        [Column("amount")]
        public decimal Amount { get; set; }

        [DataFilter]
        [ForeignKey("AgencyId")]
        public virtual AgencyEntity Agency { get; set; }
        
        [DataFilter]
        [ForeignKey("DriverId")]
        public virtual DriverEntity? Driver { get; set; }

        [DataFilter]
        [ForeignKey("DisctrictId")]
        public virtual DistrictEntity District { get; set; }

        [DataFilter]
        [ForeignKey("CityId")]
        public virtual CityEntity City { get; set; }

        [DataFilter]
        [ForeignKey("CountyId")]
        public virtual CountyEntity County { get; set; }

        [DataFilter]
        [ForeignKey("CarParameterId")]
        public virtual CarParameterEntity CarParameter { get; set; }
    }
}
