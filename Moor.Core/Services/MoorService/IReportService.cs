using Moor.Model.Dtos.MoorDto.ReportDto;

namespace Moor.Core.Services.MoorService
{
    public interface IReportService
    {
        public Task<string> GetReport(int reportType);
    }
}
