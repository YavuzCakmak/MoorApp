using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("transfer")]
    public class TransferEntity : BaseEntity
    {
        [Column("agency_id")]
        public long AgencyId { get; set; }

        [Column("driver_id")]
        public long? DriverId { get; set; }

        [Column("district_id")]
        public long DisctrictId { get; set; }
        [Column("city_id")]
        public long CityId { get; set; }
        [Column("county_id")]
        public long CountyId { get; set; }
        [Column("car_parameter_id")]
        public long CarParameterId { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("flight_code")]
        public string FlightCode { get; set; }
        [Column("explanation")]
        public string Explanation { get; set; }
        [Column("reception_type")]
        public int ReceptionType { get; set; }
        [Column("direction_type")]
        public int DirectionType { get; set; }
        [Column("departure_date")]
        public DateTime DepartureDate { get; set; }
        [Column("return_date")]
        public DateTime ReturnDate { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }

        [ForeignKey("AgencyId")]
        public virtual AgencyEntity Agency { get; set; }

        [ForeignKey("DriverId")]
        public virtual DriverEntity? Driver { get; set; }

        [ForeignKey("DisctrictId")]
        public virtual DistrictEntity District { get; set; }

        [ForeignKey("CityId")]
        public virtual CityEntity City { get; set; }

        [ForeignKey("CountyId")]
        public virtual CountyEntity County { get; set; }

        [ForeignKey("CarParameterId")]
        public virtual CarParameterEntity CarParameter { get; set; }
    }
}
