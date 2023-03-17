using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Extension.String;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Core.Utilities.DataFilter;
using Moor.Model.Dtos.MoorDto.AgencyDto;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Dtos.MoorDto.DriverDto;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Model.Models.MoorModels.DriverModel;
using Moor.Model.Models.MoorModels.DriverModel.DriverWalletModel;
using Moor.Service.Models.Dto.ResponseDto;
using Moor.Service.Services.MoorService;
using System.Net;

namespace Moor.API.Controllers
{
    [HasPermission]
    public class DriversController : CustomBaseController
    {
        private readonly IDriverService _driverService;
        private readonly ITransferService _transferService;
        private readonly IPersonnelService _personnelService;
        private readonly IMapper _mapper;

        public DriversController(IDriverService driverService, IMapper mapper, IPersonnelService personnelService, ITransferService transferService)
        {
            _driverService = driverService;
            _mapper = mapper;
            _personnelService = personnelService;
            _transferService = transferService;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var driverEntities = await _driverService.GetAllAsync(dataFilterModel);
            var driverDtos = _mapper.Map<List<DriverDto>>(driverEntities);
            if (driverDtos.IsNotNullOrEmpty())
            {
                foreach (var driverDto in driverDtos)
                {
                    var driverTotalPrice = _transferService.Where(x => x.DriverId == driverDto.Id).Sum(x => x.DriverAmount);
                    driverDto.DriverTotalPrice = driverTotalPrice;
                }
            }
            return CreateActionResult(CustomResponseDto<List<DriverDto>>.Succces((int)HttpStatusCode.OK, driverDtos));
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
