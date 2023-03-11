﻿using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;
using Moor.Model.Utilities;
using Moor.Model.Models.MoorModels.DriverModel;
using Moor.Core.Extension.String;
using Moor.Core.Enums;
using Moor.Model.Dtos.MoorDto.DriverDto;
using Moor.Model.Utilities.Authentication;
using Moor.Service.Utilities.AuthorizeHelpers;

namespace Moor.Service.Services.MoorService
{
    public class DriverService : Service<DriverEntity>, IDriverService
    {
        private readonly IPersonnelService _personnelService;
        private readonly IDriverRepository _driverRepository;
        private readonly ITransferService _transferService;
        private readonly IMapper _mapper;

        public DriverService(IGenericRepository<DriverEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IDriverRepository driverRepository, IPersonnelService personnelService, ITransferService transferService) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
            _personnelService = personnelService;
            _transferService = transferService;
        }

        public async Task<DataResult> GetDriverWallet(long driverId)
        {
            DataResult dataResult = new DataResult();
            var driverTransfer = _transferService.Where(x => x.DriverId == driverId).ToList();
            if (driverTransfer.IsNotNullOrEmpty())
            {
            }
            return dataResult;
        }

        public async Task<DataResult> Save(DriverModel driverModel)
        {
            DataResult dataResult = new DataResult();
            var personnelModel = await _personnelService.GetByIdAsync(driverModel.PersonnelId);

            if (personnelModel.IsNotNull())
            {
                DriverEntity driverEntity = new DriverEntity();
                driverEntity.PersonnelId = driverModel.PersonnelId;
                driverEntity.Status = Convert.ToInt32(Status.AKTIF);
                driverEntity.Price = driverModel.Price;
                driverEntity.IsDeleted = false;
                var driverAddResult = await base.AddAsync(driverEntity);
                if (driverAddResult.IsNotNull())
                {
                    dataResult.IsSuccess = true;
                    dataResult.PkId = driverAddResult.Id;
                    return dataResult;
                }
                else
                {
                    dataResult.IsSuccess = false;
                    return dataResult;
                }
            }
            else
            {
                dataResult.IsSuccess = false;
                dataResult.ErrorMessage = "Şöföre ait kişi bilgisi bulunamadı";
                return dataResult;
            }
        }

        public async Task<DataResult> Update(DriverDto driverDto)
        {
            DataResult dataResult = new DataResult();
            // TO-DO YAPILACAK
            //var personnelEntity = _personnelService.Where(x => x.Id == driverDto.PersonnelId).FirstOrDefault();
            //if (personnelEntity != null)
            //{
            //    personnelEntity.UpdateDate = DateTime.Now;
            //    personnelEntity.Status = Convert.ToInt32(Status.AKTIF);
            //    personnelEntity.Email = driverDto.Email;
            //    personnelEntity.FirstName = driverDto.FirstName;
            //    personnelEntity.LastName = driverDto.LastName;
            //    personnelEntity.Password = HashingHelper.CreatePasswordHash(driverDto.Password);
            //    await _personnelService.UpdateAsync(personnelEntity);
            //}
            var driverEntity = base.Where(x => x.Id == driverDto.Id).FirstOrDefault();
            if (driverEntity.IsNotNull())
            {
                driverEntity.Price = driverDto.Price;
                await base.UpdateAsync(driverEntity);
                dataResult.IsSuccess = true;
                return dataResult;
            }
            else
            {
                dataResult.IsSuccess = false;
                dataResult.ErrorMessage = "Güncellenecek kişi bulunamadı";
                return dataResult;
            }
        }
    }
}
