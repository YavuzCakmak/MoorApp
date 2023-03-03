using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Extension.String;
using Moor.Core.Repositories;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferPostDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto;
using Moor.Model.Models.MoorModels.CarParameterModel;
using Moor.Model.Models.MoorModels.CityModel;
using Moor.Model.Models.MoorModels.DistrictModel;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class TransferService : Service<TransferEntity>, ITransferService
    {
        private readonly ITravellerService _travellerService;
        private readonly IAgencyService _agencyService;
        private readonly IDriverService _driverService;
        private readonly ICityService _cityService;
        private readonly IPriceService _priceService;
        private readonly ICarParameterService _carParameterService;
        private readonly ICountyService _countyService;
        private readonly IDistrictService _districtService;
        private readonly ITransferRepository _transferRepository;
        private readonly IMapper _mapper;

        public TransferService(IGenericRepository<TransferEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, ITransferRepository transferRepository, IDistrictService districtService, IAgencyService agencyService, ICityService cityService, ICarParameterService carParameterService, IPriceService priceService, ITravellerService travellerService, ICountyService countyService, IDriverService driverService) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _transferRepository = transferRepository;
            _districtService = districtService;
            _agencyService = agencyService;
            _cityService = cityService;
            _carParameterService = carParameterService;
            _priceService = priceService;
            _travellerService = travellerService;
            _countyService = countyService;
            _driverService = driverService;
        }

        public async Task<TransferViewDto> CreateTransfer(TransferPostDto transferPostDto)
        {
            #region Objects
            TransferViewDto transferViewDto = new TransferViewDto();
            #endregion

            var agencyModel = await _agencyService.GetByIdAsync(transferPostDto.AgencyId);
            if (agencyModel.IsNull())
            {
                transferViewDto.IsSucces = false;
                transferViewDto.ErrorMessage = "Acente Bulunamadı.";
                return transferViewDto;
            }
            if (transferPostDto.TravellerDtos.IsNullOrEmpty())
            {
                transferViewDto.IsSucces = false;
                transferViewDto.ErrorMessage = "Yolcu Kayıt Etmek Zorunludur.";
                return transferViewDto;
            }
            var priceModel = _priceService.Where(x => x.CarParameterId == transferPostDto.CarParameterId && x.DistrictId == transferPostDto.DisctrictId).FirstOrDefault();
            if (priceModel.IsNull())
            {
                transferViewDto.IsSucces = false;
                transferViewDto.ErrorMessage = "Bölge ve Modele göre fiyatlandırma mevcut değildir.";
                return transferViewDto;
            }

            transferPostDto.Amount = priceModel.Price + agencyModel.ReceptionPrice;
            var transferEntity = _mapper.Map<TransferEntity>(transferPostDto);
            var transferAddResult = await base.AddAsync(transferEntity);

            if (transferAddResult.Id.IsNull())
            {
                transferViewDto.IsSucces = false;
                transferViewDto.ErrorMessage = "Transfer Kayıt Edilirken Hata Oluştu.";
                return transferViewDto;
            }
            foreach (var travellerDto in transferPostDto.TravellerDtos)
            {
                travellerDto.TransferId = transferAddResult.Id;
            }
            var travellerEntities = _mapper.Map<List<TravellerEntity>>(transferPostDto.TravellerDtos);
            var travellerAddResult = await _travellerService.AddRangeAsync(travellerEntities);


            var districtModel = _districtService.GetByIdAsync(transferPostDto.DisctrictId).Result;
            var cityModel = _cityService.GetByIdAsync(transferPostDto.CityId).Result;
            var countyName = _countyService.GetByIdAsync(transferPostDto.CountyId).Result;
            var carParameterModel = _carParameterService.GetByIdAsync(transferPostDto.CarParameterId).Result;

            transferViewDto.DistrictName = districtModel.Name;
            transferViewDto.Location = transferPostDto.Location;
            transferViewDto.FlightCode = transferPostDto.FlightCode;
            transferViewDto.CityName = cityModel.Name;
            transferViewDto.CountyName = countyName.Name;
            transferViewDto.CarParameterBrand = carParameterModel.Brand;
            transferViewDto.CarParameterModel = carParameterModel.Model;
            transferViewDto.Explanation = transferPostDto.Explanation;
            transferViewDto.DirectionType = transferPostDto.DirectionType;
            transferViewDto.ReceptionType = transferPostDto.ReceptionType;
            transferViewDto.ReturnDate = transferPostDto.ReturnDate;
            transferViewDto.DepartureDate = transferPostDto.DepartureDate;
            transferViewDto.Price = (decimal)transferPostDto.Amount;
            transferViewDto.IsSucces = true;
            return transferViewDto;
        }

        public async Task<TransferViewDto> MapTransferViewDto(TransferEntity transferEntity)
        {
            #region Objects
            TransferViewDto transferViewDto = new TransferViewDto();
            #endregion

            var districtModel = _districtService.GetByIdAsync(transferEntity.DisctrictId).Result;
            var cityModel = _cityService.GetByIdAsync(transferEntity.CityId).Result;
            var countyName = _countyService.GetByIdAsync(transferEntity.CountyId).Result;
            var carParameterModel = _carParameterService.GetByIdAsync(transferEntity.CarParameterId).Result;

            if (transferEntity.DriverId.IsNotNull())
            {
                var driverModel = _driverService.GetByIdAsync((long)transferEntity.DriverId).Result;
                transferViewDto.DriverName =  $"{driverModel.Personnel.FirstName} {driverModel.Personnel.LastName} " ;
            }

            transferViewDto.DistrictName = districtModel.Name;
            transferViewDto.Location = transferEntity.Location;
            transferViewDto.FlightCode = transferEntity.FlightCode;
            transferViewDto.CityName = cityModel.Name;
            transferViewDto.CountyName = countyName.Name;
            transferViewDto.CarParameterBrand = carParameterModel.Brand;
            transferViewDto.CarParameterModel = carParameterModel.Model;
            transferViewDto.Explanation = transferEntity.Explanation;
            transferViewDto.DirectionType = transferEntity.DirectionType;
            transferViewDto.ReceptionType = transferEntity.ReceptionType;
            transferViewDto.ReturnDate = transferEntity.ReturnDate;
            transferViewDto.DepartureDate = transferEntity.DepartureDate;
            transferViewDto.Price = (decimal)transferEntity.Amount;
            transferViewDto.IsSucces = true;
            return transferViewDto;

        }

        public async Task<List<TransferViewDto>> MapTransferViewDtos(List<TransferEntity> transferEntities)
        {
            #region Objects
            List<TransferViewDto> transferViewDtos = new List<TransferViewDto>();
            #endregion

            foreach (var transferEntity in transferEntities)
            {
                TransferViewDto transferViewDto = new TransferViewDto();

                if (transferEntity.DriverId.IsNotNull())
                {
                    var driverModel = _driverService.GetByIdAsync((long)transferEntity.DriverId).Result;
                    transferViewDto.DriverName = $"{driverModel.Personnel.FirstName} {driverModel.Personnel.LastName} ";
                }

                var districtModel = _districtService.GetByIdAsync(transferEntity.DisctrictId).Result;
                var cityModel = _cityService.GetByIdAsync(transferEntity.CityId).Result;
                var countyName = _countyService.GetByIdAsync(transferEntity.CountyId).Result;
                var carParameterModel = _carParameterService.GetByIdAsync(transferEntity.CarParameterId).Result;


                transferViewDto.DistrictName = districtModel.Name;
                transferViewDto.Location = transferEntity.Location;
                transferViewDto.FlightCode = transferEntity.FlightCode;
                transferViewDto.CityName = cityModel.Name;
                transferViewDto.CountyName = countyName.Name;
                transferViewDto.CarParameterBrand = carParameterModel.Brand;
                transferViewDto.CarParameterModel = carParameterModel.Model;
                transferViewDto.Explanation = transferEntity.Explanation;
                transferViewDto.DirectionType = transferEntity.DirectionType;
                transferViewDto.ReceptionType = transferEntity.ReceptionType;
                transferViewDto.ReturnDate = transferEntity.ReturnDate;
                transferViewDto.DepartureDate = transferEntity.DepartureDate;
                transferViewDto.Price = (decimal)transferEntity.Amount;
                transferViewDto.IsSucces = true;
                transferViewDtos.Add(transferViewDto);
            }
            return transferViewDtos;
        }
    }
}
