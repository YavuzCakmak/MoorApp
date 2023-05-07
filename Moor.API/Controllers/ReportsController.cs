using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.Core.Extension.String;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities.DataFilter;
using System.Data;

namespace Moor.API.Controllers
{

    public class ReportsController : CustomBaseController
    {
        private readonly IAgencyService _agencyService;
        private readonly ITravellerService _travellerService;
        private readonly IDistrictService _districtService;
        private readonly ITransferService _transferService;
        public ReportsController(IAgencyService agencyService, ITransferService transferService, ITravellerService travellerService, IDistrictService districtService)
        {
            _agencyService = agencyService;
            _transferService = transferService;
            _travellerService = travellerService;
            _districtService = districtService;
        }

        [HttpGet("GetReport")]
        public async void GetReport()
        {
            DataTable dataTable = new DataTable();
            var transferEntities = _transferService.Where(x => x.Status == 3).ToList();
            if (transferEntities.IsNotNullOrEmpty())
            {
                
                dataTable.Columns.Add("ACENTE_AD", typeof(string));
                dataTable.Columns.Add("UCRET", typeof(decimal));
                dataTable.Columns.Add("YOLCULAR", typeof(string));
                dataTable.Columns.Add("NEREDEN", typeof(string));
                dataTable.Columns.Add("NEREYE", typeof(string));

                foreach (var transferEntity in transferEntities)
                {
                    DataRow row = dataTable.NewRow();

                    if (transferEntity.Agency.IsNotNull())
                    {
                        row["ACENTE_AD"] = transferEntity.Agency.Name;
                    }
                    else
                    {
                        row["ACENTE_AD"] = _agencyService.Where(x => x.Id == transferEntity.AgencyId).FirstOrDefault().Name;
                    }
                    row["UCRET"] = transferEntity.AgencyAmount;

                    var travellerEntities = _travellerService.Where(x => x.TransferId == transferEntity.Id).ToList();
                    if (travellerEntities.IsNotNullOrEmpty())
                    {
                        row["YOLCULAR"] = string.Join("##", travellerEntities.Select(x => x.FirstName + "-" + x.LastName));

                    }
                    else
                    {
                        row["YOLCULAR"] = "Yolcu Mevcut Değil.";
                    }
                    row["NEREDEN"] = transferEntity.Location;
                    if (transferEntity.DisctrictId.IsNotNull())
                    {
                        row["NEREYE"] = _districtService.Where(x => x.Id == transferEntity.DisctrictId).FirstOrDefault().Name;
                    }
                    dataTable.Rows.Add(row);
                }
            }
            var xLWorkbook = new XLWorkbook();
            xLWorkbook.Worksheets.Add(dataTable,"ANKA-VİP-RAPOR");
        }
    }
}
