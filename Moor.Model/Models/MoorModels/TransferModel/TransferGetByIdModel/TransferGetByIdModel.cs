using Moor.Model.Dtos.MoorDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.TransferModel.TransferGetByIdModel
{
    public class TransferGetByIdModel
    {
        public long Id { get; set; }
        public string DistrictName { get; set; }
        public string Location { get; set; }
        public string FlightCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? DriverName { get; set; }
        public string? DriverPhoneNumber { get; set; }
        public string? Plate { get; set; }
        public string? Explanation { get; set; }
        public string? DriverMediaPath { get; set; }
        public int? DirectionType { get; set; }
        public decimal Price { get; set; }
        public string CarModel { get; set; }
        public string CarBrand { get; set; }
        public List<TravellerDto> Traveller { get; set; } = new List<TravellerDto>();
        public LookupObjectModel Status { get; set; }
    }
}
