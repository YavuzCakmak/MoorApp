using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Extension.String;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Dtos.MoorDto.CarParameterDto;
using Moor.Service.Models.Dto.ResponseDto;
using Moor.Service.Services.MoorService;
using System.Net;

namespace Moor.API.Controllers
{
    //[ValidateFilter]
    //[HasPermission]
    public class CarParametersController : CustomBaseController
    {
        private readonly ICarParameterService _carParameterService;
        private readonly IMapper _mapper;

        public CarParametersController(ICarParameterService carParameterService, IMapper mapper)
        {
            _carParameterService = carParameterService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var carParameters = await _carParameterService.GetAllAsync();
            if (carParameters.IsNotNullOrEmpty())
            {
                return CreateActionResult(CustomResponseDto<List<CarParameterDto>>.Succces((int)HttpStatusCode.OK, _mapper.Map<List<CarParameterDto>>(carParameters)));
            }
            else
            {
                return CreateActionResult(CustomResponseDto<List<NoContentDto>>.Fail((int)HttpStatusCode.NotFound));
            }
        }

        [ServiceFilter(typeof(NotFoundFilter<CarParameterEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var carParameter = await _carParameterService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<CarParameterDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarParameterDto>(carParameter)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CarParameterDto carParameterDto)
        {
            return CreateActionResult(CustomResponseDto<CarParameterDto>.Succces((int)HttpStatusCode.OK, await _carParameterService.Save(carParameterDto)));
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
