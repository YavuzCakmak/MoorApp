using Microsoft.AspNetCore.Mvc;
using Moor.Core.Services.MoorService;
using Moor.Model.Dtos.MoorDto.ReportDto;

namespace Moor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportsController(IReportService reportService)
        {
            this.reportService = reportService;
        }


        [HttpGet]
        public async Task<List<ReportDto>> All([FromQuery] int reportType)
        {
            return await reportService.GetReport(reportType);
        }
    }
}
