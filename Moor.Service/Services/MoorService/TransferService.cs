﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Enums;
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
using Moor.Model.Models.MoorModels.NotificationModel.NotificationPostModel;
using Moor.Model.Models.MoorModels.TransferModel.TransferChangeModel;
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
        private readonly ICityService _cityService;
        private readonly IPriceService _priceService;
        private readonly ICarParameterService _carParameterService;
        private readonly ICountyService _countyService;
        private readonly IDistrictService _districtService;
        private readonly ITransferRepository _transferRepository;
        private readonly IMapper _mapper;

        public TransferService(IGenericRepository<TransferEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, ITransferRepository transferRepository, IDistrictService districtService, IAgencyService agencyService, ICityService cityService, ICarParameterService carParameterService, IPriceService priceService, ITravellerService travellerService, ICountyService countyService, IDriverService driverService, INotificationService notificationService) : base(repository, unitOfWork)
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
        }

        public async Task<DataResult> ChangeTransferStatus(TransferChangeModel transferChangeModel)
        {
            #region Objects
            DataResult dataResult = new DataResult();
            #endregion

            var transferEntity = base.Where(x => x.Id == transferChangeModel.TransferId).FirstOrDefault();
            if (transferEntity.IsNotNull())
            {
                if (transferEntity.DriverId.IsNull() && transferChangeModel.DriverId.IsNotNull() && transferChangeModel.DriverId > 0)
                {
                    transferEntity.DriverId = transferChangeModel.DriverId;
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
                    transferEntity.DriverId = transferChangeModel.DriverId;
                    base.UpdateAsync(transferEntity);
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
                else if (transferEntity.Amount != transferChangeModel.Amount && transferChangeModel.Amount > 0)
                {
                    transferEntity.Amount = (decimal)transferChangeModel.Amount;
                    base.UpdateAsync(transferEntity);
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
                else if (transferEntity.Status != transferChangeModel.Status && transferChangeModel.Status != 0)
                {
                    transferEntity.Status = (int)transferChangeModel.Status;
                    base.UpdateAsync(transferEntity);
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
            transferEntity.Status = Convert.ToInt32(TransferStatus.BEKLEMEDE);
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
            var carParameterModel = _carParameterService.Where(x => x.Id == transferPostDto.CarParameterId).FirstOrDefault();

            transferViewDto.DistrictName = districtModel.Name;
            transferViewDto.Location = transferPostDto.Location;
            transferViewDto.FlightCode = transferPostDto.FlightCode;
            transferViewDto.CityName = cityModel.Name;
            transferViewDto.CountyName = countyName.Name;
            transferViewDto.CarParameterBrand = carParameterModel.CarBrand.Brand;
            transferViewDto.CarParameterModel = carParameterModel.CarModel.Model;
            transferViewDto.Explanation = transferPostDto.Explanation;
            transferViewDto.DirectionType = transferPostDto.DirectionType;
            transferViewDto.ReceptionType = transferPostDto.ReceptionType;
            transferViewDto.ReturnDate = transferPostDto.ReturnDate;
            transferViewDto.DepartureDate = transferPostDto.DepartureDate;
            transferViewDto.Price = (decimal)transferPostDto.Amount;
            transferViewDto.IsSucces = true;

            #region Notification
            NotificationPostModel notificationPostModel = new NotificationPostModel();
            notificationPostModel.AgencyId = transferPostDto.AgencyId;
            notificationPostModel.TransferId = transferAddResult.Id;
            notificationPostModel.AgencyName = agencyModel.Name;
            notificationPostModel.IsFirst = true;
            _notificationService.CreateNotification(notificationPostModel);
            #endregion

            return transferViewDto;
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
                var driverModel = _driverService.GetByIdAsync((long)transferEntity.DriverId).Result;
                transferViewDto.DriverName = $"{driverModel.Personnel.FirstName} {driverModel.Personnel.LastName} ";
            }

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

                if (transferEntity.DriverId.IsNotNull())
                {
                    var driverModel = _driverService.GetByIdAsync((long)transferEntity.DriverId).Result;
                    transferViewDto.DriverName = $"{driverModel.Personnel.FirstName} {driverModel.Personnel.LastName} ";
                }

                var districtModel = _districtService.GetByIdAsync((long)transferEntity.DisctrictId).Result;
                var cityModel = _cityService.GetByIdAsync((long)transferEntity.CityId).Result;
                var countyName = _countyService.GetByIdAsync((long)transferEntity.CountyId).Result;
                var carParameterModel = _carParameterService.Where(x => x.Id == transferEntity.CarParameterId).FirstOrDefault();

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
                transferViewDtos.Add(transferViewDto);
            }
            return transferViewDtos;
        }
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
            return lookupObjectModel;
        }
    }
}
