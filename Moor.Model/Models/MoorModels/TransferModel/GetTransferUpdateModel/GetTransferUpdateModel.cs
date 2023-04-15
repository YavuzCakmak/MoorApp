using Moor.Model.Dtos.MoorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.TransferModel.GetTransferUpdateModel
{
    public class GetTransferUpdateModel
    {
        public long? Id { get; set; }
        public List<TravellerDto> TravellerDtos { get; set; } = new List<TravellerDto>();
        public DateTime? CreateDate { get; set; }
        public string? Location { get; set; }
        public string? LocationFree { get; set; }
        public int? DirectionType { get; set; }
        public long? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public string? FlightCode { get; set; }

    }
}
