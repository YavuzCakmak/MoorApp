using Moor.Model.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto
{
    public class TransferViewDto : BaseDto.BaseDto
    {
        public string? DriverName { get; set; }
        public string DistrictName { get; set; }
        public string CityName { get; set; }
        public string CountyName { get; set; }
        public string Location { get; set; }
        public string FlightCode { get; set; }
        public string CarParameterBrand { get; set; }
        public string CarParameterModel { get; set; }
        public string Explanation { get; set; }
        public int ReceptionType { get; set; }
        public int DirectionType { get; set; }
        public LookupObjectModel Status { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal Price { get; set; }
    }
    public class LookupObjectModel
    {
        public int? Ordinal { get; set; }
        public string Text { get; set; }
    }
}
