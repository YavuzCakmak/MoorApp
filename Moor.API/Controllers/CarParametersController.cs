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
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Dtos.MoorDto.CarParameterDto;
using Moor.Model.Models.MoorModels.CarParameterModel;
using Moor.Service.Models.Dto.ResponseDto;
using Moor.Service.Services.MoorService;
using System.Net;

namespace Moor.API.Controllers
{
    [HasPermission]
    public class CarParametersController : CustomBaseController
    {
        private readonly ICarParameterService _carParameterService;
        private readonly ICarBrandService _carBrandService;
        private readonly IMapper _mapper;

        public CarParametersController(ICarParameterService carParameterService, IMapper mapper, ICarBrandService carBrandService)
        {
            _carParameterService = carParameterService;
            _mapper = mapper;
            _carBrandService = carBrandService;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var carParameters = await _carParameterService.GetAllAsync(dataFilterModel);
            return CreateActionResult(CustomResponseDto<List<CarParameterDto>>.Succces((int)HttpStatusCode.OK, _mapper.Map<List<CarParameterDto>>(carParameters)));
        }

        [ServiceFilter(typeof(NotFoundFilter<CarParameterEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var carParameter = await _carParameterService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<CarParameterDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarParameterDto>(carParameter)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CarParameterModel carParameterModel)
        {
            var dataResult = await _carParameterService.Save(carParameterModel);
            if (dataResult.IsSuccess)
            {
                var carParameterEntity = await _carParameterService.GetByIdAsync(dataResult.PkId);
                var newCarParameterDto = _mapper.Map<CarParameterDto>(carParameterEntity);
                if (newCarParameterDto.CarBrandName.IsNullOrEmpty())
                {
                    newCarParameterDto.CarBrandName = _carBrandService.Where(x => x.Id == newCarParameterDto.CarBrandId).Select(a=> a.Brand).FirstOrDefault();
                }
                return CreateActionResult(CustomResponseDto<CarParameterDto>.Succces((int)HttpStatusCode.OK, newCarParameterDto));
            }
            else
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(CarParameterDto carParameterDto)
        {
            await _carParameterService.UpdateAsync(_mapper.Map<CarParameterEntity>(carParameterDto));
            var updateCarParameterModel = await _carParameterService.GetByIdAsync(carParameterDto.Id);
            return CreateActionResult(CustomResponseDto<CarParameterDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarParameterDto>(updateCarParameterModel)));
        }
    }
}
