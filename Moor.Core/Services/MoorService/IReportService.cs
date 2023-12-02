using Moor.Model.Dtos.MoorDto.ReportDto;

namespace Moor.Core.Services.MoorService
{
    public interface IReportService
    {
        public Task<List<ReportDto>> GetReport(int reportType);
    }
}
