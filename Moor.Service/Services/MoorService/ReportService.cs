using Ganss.Excel;
using Moor.Core.Services.MoorService;
using Moor.Model.Dtos.MoorDto.ReportDto;

namespace Moor.Service.Services.MoorService
{
    public class ReportService : IReportService
    {
        private readonly ITransferService _transferService;
        private readonly ITravellerService _travellerService;
        private readonly IAgencyService _agencyService;
        private readonly IDistrictService _districtService;
        public ReportService(ITransferService transferService, IAgencyService agencyService, ITravellerService travellerService, IDistrictService districtService)
        {
            _transferService = transferService;
            _agencyService = agencyService;
            _travellerService = travellerService;
            _districtService = districtService;
        }
        public async Task<string> GetReport(int reportType)
        {
            //Report Type = 1 kümül
            //Report Type = 2 haftalık

            var excelMapper = new ExcelMapper();
            List<ReportDto> reportDtos = new List<ReportDto>();

            if (reportType == 1)
            {
                var transfers = _transferService.Where(x => x.Status == 3 && x.IsDeleted == false && x.AgencyId != 18).OrderByDescending(a => a.CreatedDate).ToList();
                if (transfers != null && transfers.Count > 0)
                {
                    foreach (var transferEntity in transfers)
                    {
                        ReportDto reportDto = new ReportDto();

                        reportDto.Tarih = transferEntity.CreatedDate;
                        reportDto.AcenteAd = transferEntity.Agency != null && !string.IsNullOrWhiteSpace(transferEntity.Agency.Name) ? transferEntity.Agency.Name : _agencyService.Where(x => x.Id == transferEntity.AgencyId).FirstOrDefault().Name;
                        reportDto.Ücret = transferEntity.AgencyAmount.Value;

                        var yolcular = _travellerService.Where(x => x.TransferId == transferEntity.Id).ToList();

                        if (yolcular != null && yolcular.Count > 0)
                        {
                            reportDto.Yolcular = new List<Yolcu>();

                            foreach (var travellerEntity in yolcular)
                            {
                                Yolcu yolcu = new Yolcu();
                                yolcu.Ad = travellerEntity.FirstName;
                                yolcu.Soyad = travellerEntity.LastName;
                                reportDto.Yolcular.Add(yolcu);
                            }
                        }

                        reportDto.Nereden = transferEntity.DirectionType == 1 ? transferEntity.Location : (transferEntity.District != null && !string.IsNullOrWhiteSpace(transferEntity.District.Name) ? transferEntity.District.Name : _districtService.Where(x => x.Id == transferEntity.DisctrictId).FirstOrDefault().Name);
                        reportDto.Nereye = transferEntity.DirectionType == 2 ? transferEntity.Location : (transferEntity.District != null && !string.IsNullOrWhiteSpace(transferEntity.District.Name) ? transferEntity.District.Name : _districtService.Where(x => x.Id == transferEntity.DisctrictId).FirstOrDefault().Name);


                        reportDtos.Add(reportDto);
                    }
                }
            }
            if (reportType == 2)
            {
                DateTime currentDate = DateTime.Now;

                DateTime firstDayOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
                DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

                var transfers = _transferService.Where(x => x.Status == 3 
                                                    && x.IsDeleted == false 
                                                    && x.AgencyId != 18
                                                    && (x.CreatedDate >= firstDayOfWeek && x.CreatedDate <= lastDayOfWeek)
                                                    ).OrderByDescending(a => a.CreatedDate).ToList();


                if (transfers != null && transfers.Count > 0)
                {
                    foreach (var transferEntity in transfers)
                    {
                        ReportDto reportDto = new ReportDto();

                        reportDto.Tarih = transferEntity.CreatedDate;
                        reportDto.AcenteAd = transferEntity.Agency != null && !string.IsNullOrWhiteSpace(transferEntity.Agency.Name) ? transferEntity.Agency.Name : _agencyService.Where(x => x.Id == transferEntity.AgencyId).FirstOrDefault().Name;
                        reportDto.Ücret = transferEntity.AgencyAmount.Value;

                        var yolcular = _travellerService.Where(x => x.TransferId == transferEntity.Id).ToList();

                        if (yolcular != null && yolcular.Count > 0)
                        {
                            reportDto.Yolcular = new List<Yolcu>();

                            foreach (var travellerEntity in yolcular)
                            {
                                Yolcu yolcu = new Yolcu();
                                yolcu.Ad = travellerEntity.FirstName;
                                yolcu.Soyad = travellerEntity.LastName;
                                reportDto.Yolcular.Add(yolcu);
                            }
                        }

                        reportDto.Nereden = transferEntity.DirectionType == 1 ? transferEntity.Location : (transferEntity.District != null && !string.IsNullOrWhiteSpace(transferEntity.District.Name) ? transferEntity.District.Name : _districtService.Where(x => x.Id == transferEntity.DisctrictId).FirstOrDefault().Name);
                        reportDto.Nereye = transferEntity.DirectionType == 2 ? transferEntity.Location : (transferEntity.District != null && !string.IsNullOrWhiteSpace(transferEntity.District.Name) ? transferEntity.District.Name : _districtService.Where(x => x.Id == transferEntity.DisctrictId).FirstOrDefault().Name);


                        reportDtos.Add(reportDto);
                    }
                }
            }


            List<ExcelReportModel> excelData = reportDtos.Select(dto => new ExcelReportModel
            {
                Tarih = dto.Tarih,
                AcenteAd = dto.AcenteAd,
                Ücret = dto.Ücret,
                Yolcular = string.Join(", ", dto.Yolcular.Select(y => $"{y.Ad} {y.Soyad}")),
                Nereden = dto.Nereden,
                Nereye = dto.Nereye
            }).ToList();

            var memoryStream = new MemoryStream();
            string fileName = $"{Guid.NewGuid()}.xlsx";
            excelMapper.Save(memoryStream, excelData, "Rapor");

            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Report");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (FileStream stream = new FileStream(Path.Combine(directoryPath, fileName), FileMode.Create))
            {
                memoryStream.WriteTo(stream);
            }

            string filePath = Path.Combine(directoryPath, fileName);

            var returnFileName = $"https://backend.ankavip.com.tr/Report/{fileName}";
            return returnFileName;
        }

        public class ExcelReportModel
        {
            public DateTime Tarih { get; set; }
            public string AcenteAd { get; set; }
            public decimal Ücret { get; set; }
            public string Yolcular { get; set; }
            public string Nereden { get; set; }
            public string Nereye { get; set; }
        }
    }
}
