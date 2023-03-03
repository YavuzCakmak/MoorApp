using Moor.Model.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.TransferModel
{
    public class TransferModel : BaseModel
    {
        public long AgencyId { get; set; }
        public long? DriverId { get; set; }
        public long DisctrictId { get; set; }
        public long CityId { get; set; }
        public long CountyId { get; set; }
        public long CarParameterId { get; set; }
        public string Location { get; set; }
        public string FlightCode { get; set; }
        public string Explanation { get; set; }
        public int ReceptionType { get; set; }
        public int DirectionType { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Amount { get; set; }
    }
}
