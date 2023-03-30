using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Enums;
using Moor.Core.Extension.String;
using Moor.Core.Repositories;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Model.Dtos.MoorDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferPostDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto;
using Moor.Model.Models.MoorModels.AgencyModel.AgencyWalletModel;
using Moor.Model.Models.MoorModels.AgencyModel.DebitForAgencyModel;
using Moor.Model.Models.MoorModels.CarParameterModel;
using Moor.Model.Models.MoorModels.CityModel;
using Moor.Model.Models.MoorModels.DistrictModel;
using Moor.Model.Models.MoorModels.DriverCarModel;
using Moor.Model.Models.MoorModels.DriverModel;
using Moor.Model.Models.MoorModels.DriverModel.DebitForDriverModel;
using Moor.Model.Models.MoorModels.DriverModel.DriverWalletModel;
using Moor.Model.Models.MoorModels.NotificationModel.NotificationPostModel;
using Moor.Model.Models.MoorModels.TransferModel.TransferChangeModel;
using Moor.Model.Models.MoorModels.TransferModel.TransferGetByIdModel;
using Moor.Model.Utilities;
using Moor.Service.Services.BaseService;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Moor.Service.Services.MoorService
{
    public class TransferService : Service<TransferEntity>, ITransferService
    {
        private readonly ITravellerService _travellerService;
        private readonly INotificationService _notificationService;
        private readonly IAgencyService _agencyService;
        private readonly IDriverService _driverService;
        private readonly IDriverCarService _driverCarService;
        private readonly ICityService _cityService;
        private readonly IPriceService _priceService;
        private readonly ICarParameterService _carParameterService;
        private readonly ICountyService _countyService;
        private readonly IDistrictService _districtService;
        private readonly ITransferRepository _transferRepository;
        private readonly IMapper _mapper;

        public TransferService(IGenericRepository<TransferEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, ITransferRepository transferRepository, IDistrictService districtService, IAgencyService agencyService, ICityService cityService, ICarParameterService carParameterService, IPriceService priceService, ITravellerService travellerService, ICountyService countyService, IDriverService driverService, INotificationService notificationService, IDriverCarService driverCarService) : base(repository, unitOfWork)
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
            _notificationService = notificationService;
            _driverCarService = driverCarService;
        }


        #region Transfer
        public async Task<DataResult> ChangeTransferStatus(TransferChangeModel transferChangeModel)
        {
            #region Objects
            DataResult dataResult = new DataResult();
            dataResult.IsSuccess = true;
            #endregion

            var transferEntity = base.Where(x => x.Id == transferChangeModel.TransferId).FirstOrDefault();
            if (transferEntity.IsNotNull())
            {
                if (transferEntity.DriverId.IsNull() && transferChangeModel.DriverId.IsNotNull() && transferChangeModel.DriverId > 0)
                {
                    var driverModel = _driverService.Where(x => x.Id == transferChangeModel.DriverId).FirstOrDefault();
                    transferEntity.DriverId = transferChangeModel.DriverId;
                    transferEntity.DriverAmount = driverModel.Price;
                    await base.UpdateAsync(transferEntity);
                    dataResult.IsSuccess = true;
                    #region Notification
                    NotificationPostModel notificationPostModel = new NotificationPostModel();
                    notificationPostModel.TransferId = transferChangeModel.TransferId;
                    notificationPostModel.AgencyName = transferEntity.Agency.Name;
                    notificationPostModel.AgencyId = transferEntity.AgencyId;
                    notificationPostModel.IsDriver = true;
                    await _notificationService.CreateNotification(notificationPostModel);
                    #endregion
                    return dataResult;
                }
                else if (transferEntity.DriverId.IsNotNull() && transferChangeModel.DriverId.IsNotNull() && transferChangeModel.DriverId > 0 && transferEntity.DriverId != transferChangeModel.DriverId)
                {
                    var driverModel = _driverService.Where(x => x.Id == transferChangeModel.DriverId).FirstOrDefault();
                    transferEntity.DriverId = transferChangeModel.DriverId;
                    transferEntity.DriverAmount = driverModel.Price;
                    await base.UpdateAsync(transferEntity);
                    #region Notification
                    NotificationPostModel notificationPostModel = new NotificationPostModel();
                    notificationPostModel.TransferId = transferChangeModel.TransferId;
                    notificationPostModel.AgencyId = transferEntity.AgencyId;
                    notificationPostModel.AgencyName = transferEntity.Agency.Name;
                    notificationPostModel.IsDriverChange = true;
                    await _notificationService.CreateNotification(notificationPostModel);
                    #endregion
                    dataResult.IsSuccess = true;
                    return dataResult;
                }
                else if (transferChangeModel.Amount.IsNotNull() && transferEntity.Amount != transferChangeModel.Amount)
                {
                    transferEntity.Amount = (decimal)transferChangeModel.Amount;
                    transferEntity.AgencyAmount = (decimal)transferChangeModel.Amount;
                    await base.UpdateAsync(transferEntity);
                    dataResult.IsSuccess = true;
                    #region Notification
                    NotificationPostModel notificationPostModel = new NotificationPostModel();
                    notificationPostModel.TransferId = transferChangeModel.TransferId;
                    notificationPostModel.AgencyId = transferEntity.AgencyId;
                    notificationPostModel.AgencyName = transferEntity.Agency.Name;
                    notificationPostModel.IsAmount = true;
                    await _notificationService.CreateNotification(notificationPostModel);
                    #endregion
                    return dataResult;
                }
                else if (transferChangeModel.Status.IsNotNull() && transferEntity.Status != transferChangeModel.Status && transferChangeModel.Status != 0)
                {
                    transferEntity.Status = (int)transferChangeModel.Status;
                    await base.UpdateAsync(transferEntity);
                    dataResult.IsSuccess = true;
                    #region Notification
                    NotificationPostModel notificationPostModel = new NotificationPostModel();
                    notificationPostModel.TransferId = transferChangeModel.TransferId;
                    notificationPostModel.AgencyId = transferEntity.AgencyId;
                    notificationPostModel.AgencyName = transferEntity.Agency.Name;
                    notificationPostModel.IsStatus = true;
                    await _notificationService.CreateNotification(notificationPostModel);
                    #endregion
                    return dataResult;
                }
            }
            else
            {
                dataResult.ErrorMessage = "Transfer Bulunamadı.";
                dataResult.IsSuccess = false;
                return dataResult;
            }
            return dataResult;
        }
        public async Task<DataResult> CreateTransfer(TransferPostDto transferPostDto)
        {
            #region Objects
            TransferViewDto transferViewDto = new TransferViewDto();
            DataResult dataResult = new DataResult();
            #endregion

            var agencyModel = await _agencyService.GetByIdAsync(transferPostDto.AgencyId);
            if (agencyModel.IsNull())
            {
                dataResult.IsSuccess = false;
                dataResult.ErrorMessage = "Acente Bulunamadı.";
                return dataResult;
            }
            if (transferPostDto.TravellerDtos.IsNullOrEmpty())
            {
                dataResult.IsSuccess = false;
                dataResult.ErrorMessage = "Yolcu Kayıt Etmek Zorunludur.";
                return dataResult;
            }
            var priceModel = _priceService.Where(x => x.CarParameterId == transferPostDto.CarParameterId && x.DistrictId == transferPostDto.DisctrictId).FirstOrDefault();
            if (priceModel.IsNull())
            {
                dataResult.IsSuccess = false;
                dataResult.ErrorMessage = "Bölge ve Modele göre fiyatlandırma mevcut değildir.";
                return dataResult;
            }
            if (transferPostDto.DirectionType == Convert.ToInt32(DirectionType.TEK_YON))
            {
                transferPostDto.Amount = priceModel.Price;
                transferPostDto.FlightCode = "UCUS KODU YOK";
                transferPostDto.ReceptionType = 3;
            }
            else
            {
                transferPostDto.Amount = priceModel.Price + agencyModel.ReceptionPrice;
            }
           
            var transferEntity = _mapper.Map<TransferEntity>(transferPostDto);
            if (transferPostDto.LocationFree.IsNotNullOrEmpty())
            {
                transferEntity.LocationFree = transferPostDto.LocationFree;
            }
            transferEntity.CreatedDate = transferPostDto.ReturnDate.Value;
            transferEntity.AgencyAmount = transferPostDto.Amount;
            transferEntity.Status = Convert.ToInt32(TransferStatus.BEKLEMEDE);
            var transferAddResult = await base.AddAsync(transferEntity);
            if (transferAddResult.Id.IsNull())
            {
                dataResult.IsSuccess = false;
                dataResult.ErrorMessage = "Transfer Kayıt Edilirken Hata Oluştu.";
                return dataResult;
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
            var carParameterModel = _carParameterService.Where(x => x.Id == transferPostDto.CarParameterId).FirstOrDefault();

            transferViewDto.DistrictName = districtModel.Name.IsNotNullOrEmpty() ? districtModel.Name : "";
            transferViewDto.Location = transferPostDto.Location.IsNotNullOrEmpty() ? transferPostDto.Location : "";
            transferViewDto.FlightCode = transferPostDto.FlightCode.IsNotNullOrEmpty() ? transferPostDto.FlightCode : "";
            transferViewDto.CityName = cityModel.Name.IsNotNullOrEmpty() ? cityModel.Name : "";
            transferViewDto.CountyName = countyName.Name.IsNotNullOrEmpty() ? countyName.Name : "";
            transferViewDto.CarParameterBrand = carParameterModel.CarBrand.Brand.IsNotNullOrEmpty() ? carParameterModel.CarBrand.Brand : "";
            transferViewDto.CarParameterModel = carParameterModel.CarModel.Model.IsNotNullOrEmpty() ? carParameterModel.CarModel.Model : "";
            transferViewDto.Explanation = transferPostDto.Explanation.IsNotNullOrEmpty() ? transferPostDto.Explanation : ""; ;
            transferViewDto.DirectionType = transferPostDto.DirectionType;
            transferViewDto.ReceptionType = transferPostDto.ReceptionType;
            transferViewDto.ReturnDate = transferPostDto.ReturnDate;
            transferViewDto.DepartureDate = transferPostDto.DepartureDate;
            transferViewDto.Price = (decimal)transferPostDto.Amount;
            transferViewDto.IsSucces = true;
            dataResult.IsSuccess = true;
            dataResult.PkId = transferAddResult.Id;

            #region Notification
            NotificationPostModel notificationPostModel = new NotificationPostModel();
            notificationPostModel.AgencyId = transferPostDto.AgencyId;
            notificationPostModel.TransferId = transferAddResult.Id;
            notificationPostModel.AgencyName = agencyModel.Name;
            notificationPostModel.IsFirst = true;
            await _notificationService.CreateNotification(notificationPostModel);
            #endregion

            return dataResult;
        }

        public async Task<TransferViewDto> MapTransferViewDto(TransferEntity transferEntity)
        {
            #region Objects
            TransferViewDto transferViewDto = new TransferViewDto();
            #endregion

            var districtModel = _districtService.GetByIdAsync((long)transferEntity.DisctrictId).Result;
            var cityModel = _cityService.GetByIdAsync((long)transferEntity.CityId).Result;
            var countyName = _countyService.GetByIdAsync((long)transferEntity.CountyId).Result;
            var carParameterModel = _carParameterService.Where(x => x.Id == transferEntity.CarParameterId).FirstOrDefault();

            if (transferEntity.DriverId.IsNotNull())
            {
                var driverModel = _driverService.Where(x => x.Id == transferEntity.DriverId).FirstOrDefault();
                var driverCarModel = _driverCarService.Where(x => x.DriverId == transferEntity.DriverId).FirstOrDefault();
                transferViewDto.DriverName = $"{driverModel.Personnel.FirstName} {driverModel.Personnel.LastName} ";
                transferViewDto.DriverAmount = driverModel.Price;
                transferViewDto.DriverPhoneNumber = driverModel.Personnel.PhoneNumber;
                transferViewDto.Plate = driverCarModel.Car.NumberPlate;
                transferViewDto.DriverMediaPath = driverModel.Personnel.MediaPath.IsNotNullOrEmpty() ? driverModel.Personnel.MediaPath : string.Empty;

                using (FileStream stream = new FileStream(driverModel.Personnel.MediaPath, FileMode.Open))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    string base64Data = Convert.ToBase64String(bytes);
                    transferViewDto.DriverMediaPath = base64Data;
                }
            }

            transferViewDto.Id = transferEntity.Id;
            transferViewDto.CreatedDate = transferEntity.CreatedDate;
            transferViewDto.DistrictName = districtModel.Name;
            transferViewDto.Location = transferEntity.Location;
            transferViewDto.FlightCode = transferEntity.FlightCode;
            transferViewDto.CityName = cityModel.Name;
            transferViewDto.CountyName = countyName.Name;
            transferViewDto.CarParameterBrand = carParameterModel.CarBrand.Brand;
            transferViewDto.CarParameterModel = carParameterModel.CarModel.Model;
            transferViewDto.Explanation = transferEntity.Explanation;
            transferViewDto.DirectionType = (int)transferEntity.DirectionType;
            transferViewDto.ReceptionType = (int)transferEntity.ReceptionType;
            transferViewDto.ReturnDate = transferEntity.ReturnDate;
            transferViewDto.DepartureDate = transferEntity.DepartureDate;
            transferViewDto.Price = (decimal)transferEntity.Amount;
            transferViewDto.AgencyAmount = (decimal)transferEntity.AgencyAmount;
            transferViewDto.Status = GetLookup(transferEntity.Status);
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

                var travellers = _travellerService.Where(x => x.TransferId == transferEntity.Id).ToList();
                if (transferEntity.DriverId.IsNotNull())
                {
                    var driverModel = _driverService.Where(x => x.Id == transferEntity.DriverId).FirstOrDefault();
                    var driverCarModel = _driverCarService.Where(x => x.DriverId == transferEntity.DriverId).FirstOrDefault();
                    transferViewDto.DriverName = $"{driverModel.Personnel.FirstName} {driverModel.Personnel.LastName} ";
                    transferViewDto.DriverAmount = transferEntity.DriverAmount;
                    transferViewDto.DriverPhoneNumber = driverModel.Personnel.PhoneNumber;
                    transferViewDto.Plate = driverCarModel.Car.NumberPlate;

                    transferViewDto.DriverMediaPath = driverModel.Personnel.MediaPath.IsNotNullOrEmpty() ? driverModel.Personnel.MediaPath : string.Empty;

                    using (FileStream stream = new FileStream(driverModel.Personnel.MediaPath, FileMode.Open))
                    {
                        byte[] bytes = new byte[stream.Length];
                        stream.Read(bytes, 0, bytes.Length);
                        string base64Data = Convert.ToBase64String(bytes);
                        transferViewDto.DriverMediaPath = base64Data;
                    }
                }

                var districtModel = _districtService.GetByIdAsync((long)transferEntity.DisctrictId).Result;
                var cityModel = _cityService.GetByIdAsync((long)transferEntity.CityId).Result;
                var countyName = _countyService.GetByIdAsync((long)transferEntity.CountyId).Result;
                var carParameterModel = _carParameterService.Where(x => x.Id == transferEntity.CarParameterId).FirstOrDefault();

                transferViewDto.Id = transferEntity.Id;
                transferViewDto.AgencyName = transferEntity.Agency.Name;
                using (FileStream stream = new FileStream(transferEntity.Agency.MediaPath, FileMode.Open))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    string base64Data = Convert.ToBase64String(bytes);
                    transferViewDto.AgencyMediaPath = base64Data;
                }
                if (travellers.IsNotNullOrEmpty())
                {
                    foreach (var item in travellers)
                    {
                        var dto = _mapper.Map<TravellerDto>(item);
                        transferViewDto.TravellerDtos.Add(dto);
                    }
                }
                transferViewDto.CreatedDate = transferEntity.CreatedDate;
                transferViewDto.DistrictName = districtModel.Name;
                transferViewDto.Location = transferEntity.Location;
                transferViewDto.FlightCode = transferEntity.FlightCode;
                transferViewDto.CityName = cityModel.Name;
                transferViewDto.CountyName = countyName.Name;
                transferViewDto.CarParameterBrand = carParameterModel.CarBrand.Brand;
                transferViewDto.CarParameterModel = carParameterModel.CarModel.Model;
                transferViewDto.Explanation = transferEntity.Explanation;
                transferViewDto.DirectionType = (int)transferEntity.DirectionType;
                transferViewDto.ReceptionType = (int)transferEntity.ReceptionType;
                transferViewDto.ReturnDate = transferEntity.ReturnDate;
                transferViewDto.Status = GetLookup(transferEntity.Status);
                transferViewDto.DepartureDate = transferEntity.DepartureDate;
                transferViewDto.Price = (decimal)transferEntity.Amount;
                transferViewDto.IsSucces = true;
                transferViewDto.AgencyAmount = (decimal)transferEntity.AgencyAmount;
                transferViewDtos.Add(transferViewDto);
            }
            return transferViewDtos;
        }
        #endregion

        #region WalletViewService
        public async Task<AgencyWalletModel> GetAgencyWallet(long agencyId)
        {
            AgencyWalletModel agencyWalletModel = new AgencyWalletModel();
            var driverTransfer = base.Where(x => x.AgencyId == agencyId && x.Status == Convert.ToInt32(TransferStatus.TAMAMLANDI)).ToList();
            if (driverTransfer.IsNotNullOrEmpty())
            {
                agencyWalletModel.AgencyTotalAmount = (decimal)driverTransfer.Sum(x => x.AgencyAmount);
            }
            return agencyWalletModel;
        }

        public async Task<DriverWalletModel> GetDriverWallet(long driverId)
        {
            DriverWalletModel driverWalletModel = new DriverWalletModel();
            var driverTransfer = base.Where(x => x.DriverId == driverId && x.Status == Convert.ToInt32(TransferStatus.TAMAMLANDI)).ToList();
            if (driverTransfer.IsNotNullOrEmpty())
            {
                driverWalletModel.DriverTotalAmount = (decimal)driverTransfer.Sum(x => x.DriverAmount);
            }
            return driverWalletModel;
        }
        #endregion

        #region WalletEditService
        public async Task<DataResult> AddDebitForDriver(DebitForDriverModel debitForDriverModel)
        {
            #region Objects
            DataResult dataResult = new DataResult();
            #endregion
            if (debitForDriverModel.TransferId.IsNotNull() && debitForDriverModel.DriverId.IsNotNull() && debitForDriverModel.Amount.IsNotNull())
            {
                var transferModel = base.Where(x => x.Id == debitForDriverModel.TransferId && x.DriverId == debitForDriverModel.DriverId).FirstOrDefault();
                if (transferModel.IsNotNull())
                {
                    transferModel.DriverAmount = transferModel.DriverAmount + debitForDriverModel.Amount;
                    if (transferModel.DriverAmount < 0)
                    {
                        dataResult.IsSuccess = false;
                        dataResult.ErrorMessage = "Hatalı tutar girişi yaptınız.";
                        return dataResult;
                    }
                    else
                    {
                        await base.UpdateAsync(transferModel);
                        dataResult.IsSuccess = true;
                        return dataResult;
                    }
                }
                else
                {
                    dataResult.IsSuccess = false;
                    dataResult.ErrorMessage = "Transfer bilgisi bulunamadı.";
                    return dataResult;
                }
            }
            return dataResult;
        }

        public async Task<DataResult> AddDebitForAgency(DebitForAgencyModel debitForAgencyModel)
        {
            #region Objects
            DataResult dataResult = new DataResult();
            #endregion
            if (debitForAgencyModel.TransferId.IsNotNull() && debitForAgencyModel.AgencyId.IsNotNull() && debitForAgencyModel.Amount.IsNotNull())
            {
                var transferModel = base.Where(x => x.Id == debitForAgencyModel.TransferId && x.AgencyId == debitForAgencyModel.AgencyId).FirstOrDefault();
                if (transferModel.IsNotNull())
                {
                    transferModel.AgencyAmount = transferModel.AgencyAmount + debitForAgencyModel.Amount;
                    if (transferModel.AgencyAmount < 0)
                    {
                        dataResult.IsSuccess = false;
                        dataResult.ErrorMessage = "Hatalı tutar girişi yaptınız.";
                        return dataResult;
                    }
                    else
                    {
                        await base.UpdateAsync(transferModel);
                        dataResult.IsSuccess = true;
                        return dataResult;
                    }
                }
                else
                {
                    dataResult.IsSuccess = false;
                    dataResult.ErrorMessage = "Transfer bilgisi bulunamadı.";
                    return dataResult;
                }
            }
            return dataResult;
        }
        #endregion

        private LookupObjectModel GetLookup(int transferStatus)
        {
            LookupObjectModel lookupObjectModel = new LookupObjectModel();
            if (transferStatus == 0)
            {
                lookupObjectModel.Text = "BEKLEMDE";
                lookupObjectModel.Ordinal = Convert.ToInt32(TransferStatus.BEKLEMEDE);
                return lookupObjectModel;
            }
            if (transferStatus == 1)
            {
                lookupObjectModel.Text = "IPTAL";
                lookupObjectModel.Ordinal = Convert.ToInt32(TransferStatus.IPTAL);
                return lookupObjectModel;
            }
            if (transferStatus == 2)
            {
                lookupObjectModel.Text = "NON-SHOW";
                lookupObjectModel.Ordinal = Convert.ToInt32(TransferStatus.NON_SHOW);
                return lookupObjectModel;
            }
            if (transferStatus == 3)
            {
                lookupObjectModel.Text = "TAMAMLANDI";
                lookupObjectModel.Ordinal = Convert.ToInt32(TransferStatus.TAMAMLANDI);
                return lookupObjectModel;
            }
            if (transferStatus == 4)
            {
                lookupObjectModel.Text = "YOLCU GELMEDI";
                lookupObjectModel.Ordinal = Convert.ToInt32(TransferStatus.YOLCU_GELMEDI);
                return lookupObjectModel;
            }
            return lookupObjectModel;
        }

        public async Task<TransferGetByIdModel> GetTransferDetail(long transferId)
        {
            #region Object
            TransferGetByIdModel transferGetByIdModel = new TransferGetByIdModel();
            #endregion
            var transferEntity = base.Where(x => x.Id == transferId).FirstOrDefault();
            var districtModel = _districtService.GetByIdAsync((long)transferEntity.DisctrictId).Result;

            transferGetByIdModel.DistrictName = districtModel.Name.IsNotNullOrEmpty() ? districtModel.Name : "";
            transferGetByIdModel.Location = transferEntity.Location.IsNotNullOrEmpty() ? transferEntity.Location : "";
            transferGetByIdModel.FlightCode = transferEntity.FlightCode.IsNotNullOrEmpty() ? transferEntity.FlightCode : "";
            transferGetByIdModel.Status = GetLookup(transferEntity.Status);
            transferGetByIdModel.CreatedDate = transferEntity.CreatedDate;
            if (transferEntity.DriverId.IsNotNull())
            {
                var driverModel = _driverService.Where(x => x.Id == (long)transferEntity.DriverId).FirstOrDefault();
                var driverCarModel = _driverCarService.Where(x => x.DriverId == transferEntity.DriverId).FirstOrDefault();
                transferGetByIdModel.DriverName = $"{driverModel.Personnel.FirstName} {driverModel.Personnel.LastName} ";
                if (driverCarModel.IsNotNull())
                {
                    transferGetByIdModel.Plate = driverCarModel.Car.NumberPlate;
                }
                using (FileStream stream = new FileStream(driverModel.Personnel.MediaPath, FileMode.Open))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    string base64Data = Convert.ToBase64String(bytes);
                    transferGetByIdModel.DriverMediaPath = base64Data;
                }
                transferGetByIdModel.DriverPhoneNumber = driverModel.Personnel.PhoneNumber.IsNotNullOrEmpty() ? driverModel.Personnel.PhoneNumber : "";
            }
            transferGetByIdModel.Price = (decimal)transferEntity.Amount;
            transferGetByIdModel.Id = transferEntity.Id;
            var carParameterModel = _carParameterService.Where(x => x.Id == transferEntity.CarParameterId).FirstOrDefault();
            transferGetByIdModel.CarBrand = carParameterModel.CarBrand.Brand;
            transferGetByIdModel.CarModel = carParameterModel.CarModel.Model;

            var travellers = _travellerService.Where(x => x.TransferId == transferEntity.Id).ToList();
            var travellersDto = _mapper.Map<List<TravellerDto>>(travellers);
            foreach (var travellerEntity in travellersDto)
            {
                transferGetByIdModel.Traveller.Add(travellerEntity);
            }
            return transferGetByIdModel;
        }
    }
}
