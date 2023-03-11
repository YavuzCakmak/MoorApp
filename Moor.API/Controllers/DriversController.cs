﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Extension.String;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Core.Utilities.DataFilter;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Dtos.MoorDto.DriverDto;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Model.Models.MoorModels.DriverModel;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Controllers
{
    [HasPermission]
    public class DriversController : CustomBaseController
    {
        private readonly IDriverService _driverService;
        private readonly IPersonnelService _personnelService;
        private readonly IMapper _mapper;

        public DriversController(IDriverService driverService, IMapper mapper, IPersonnelService personnelService)
        {
            _driverService = driverService;
            _mapper = mapper;
            _personnelService = personnelService;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var driverEntities = await _driverService.GetAllAsync(dataFilterModel);
            var driverDtos = _mapper.Map<List<DriverDto>>(driverEntities);
            return CreateActionResult(CustomResponseDto<List<DriverDto>>.Succces((int)HttpStatusCode.OK, driverDtos));
        }

        [HttpGet("GetDriverWallet")]
        public async Task<IActionResult> GetDriverWallet([FromQuery] long driverId)
        {
            return CreateActionResult(CustomResponseDto<DriverDto>.Succces((int)HttpStatusCode.OK));
        }

        [ServiceFilter(typeof(NotFoundFilter<DriverEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var driverEntity = await _driverService.GetByIdAsync(id);
            if (driverEntity.IsNotNull())
            {
                var personnelModel = _personnelService.Where(x => x.Id == driverEntity.PersonnelId).FirstOrDefault();
                driverEntity.Personnel = personnelModel;
            }
            return CreateActionResult(CustomResponseDto<DriverDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<DriverDto>(driverEntity)));
        }

        [HttpPost]
        [ValidateFilter]
        public async Task<IActionResult> Save(DriverModel driverModel)
        {
            var dataResult = await _driverService.Save(driverModel);
            if (dataResult.IsSuccess)
            {
                var driverEntity = _driverService.Where(x => x.Id == dataResult.PkId).FirstOrDefault();
                var driverDto = _mapper.Map<DriverDto>(driverEntity);
                return CreateActionResult(CustomResponseDto<DriverDto>.Succces((int)HttpStatusCode.OK, driverDto));
            }
            else
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, dataResult.ErrorMessages));
        }

        [HttpPut]
        public async Task<IActionResult> Update(DriverDto driverDto)
        {
            var dataResult = await _driverService.Update(driverDto);
            if (dataResult.IsSuccess)
            {
                var driverEntity = await _driverService.GetByIdAsync(driverDto.Id);
                var driverNewDto = _mapper.Map<DriverDto>(driverEntity);
                return CreateActionResult(CustomResponseDto<DriverDto>.Succces((int)HttpStatusCode.OK, driverNewDto));
            }
            else
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, dataResult.ErrorMessages));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            var driverEntity = await _driverService.GetByIdAsync(id);
            await _driverService.RemoveAsync(driverEntity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        }
    }
}
