using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Repository.Entities
{
    public class TransferEntity
    {
        [Column("agency_id")]
        public long AgencyId { get; set; }
        [Column("disctrict_id")]
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
    }
}
