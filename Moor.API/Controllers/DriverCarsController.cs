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
using Moor.Model.Dtos.MoorDto;
using Moor.Model.Dtos.MoorDto.CarBrandDto;
using Moor.Model.Dtos.MoorDto.DriverDto;
using Moor.Model.Models.MoorModels.DriverCarModel;
using Moor.Model.Models.MoorModels.DriverModel;
using Moor.Service.Models.Dto.ResponseDto;
using Moor.Service.Services.MoorService;
using System.Net;

namespace Moor.API.Controllers
{
    //[HasPermission]
    public class DriverCarsController : CustomBaseController
    {
        private readonly IDriverCarService _driverCarService;
        private readonly IDriverService _driverService;
        private readonly IMapper _mapper;

        public DriverCarsController(IDriverCarService driverCarService, IMapper mapper, IDriverService driverService)
        {
            _driverCarService = driverCarService;
            _mapper = mapper;
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var driverCarEntities = await _driverCarService.GetAllAsync(dataFilterModel);
            var driverCarDtos = _mapper.Map<List<DriverCarDto>>(driverCarEntities);
            return CreateActionResult(CustomResponseDto<List<DriverCarDto>>.Succces((int)HttpStatusCode.OK, driverCarDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<DriverCarEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var carBrandEntity = await _driverCarService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<DriverCarDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<DriverCarDto>(carBrandEntity)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(DriverModel driverModel)
        {
            var dataResult = await _driverCarService.Save(driverModel);
            if (dataResult.IsSuccess)
            {
                var driverEntity = _driverService.Where(x => x.Id == dataResult.PkId).FirstOrDefault();
                var driverDto = _mapper.Map<DriverDto>(driverEntity);
                return CreateActionResult(CustomResponseDto<DriverDto>.Succces((int)HttpStatusCode.OK, driverDto));
            }
            else
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, dataResult.ErrorMessages));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            var driverCarEntity = await _driverCarService.GetByIdAsync(id);
            await _driverCarService.RemoveAsync(driverCarEntity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        }
    }
}
