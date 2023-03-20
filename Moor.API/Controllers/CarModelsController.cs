using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Core.Utilities.DataFilter;
using Moor.Model.Dtos.MoorDto.CarModelDto;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Controllers
{
    //[HasPermission]
    public class CarModelsController : CustomBaseController
    {
        private readonly ICarModelService _carModelService;
        private readonly IMapper _mapper;

        public CarModelsController(ICarModelService carModelService, IMapper mapper)
        {
            _carModelService = carModelService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var carModelEntities = await _carModelService.GetAllAsync(dataFilterModel);
            var carModelDtos = _mapper.Map<List<CarModelDto>>(carModelEntities);
            return CreateActionResult(CustomResponseDto<List<CarModelDto>>.Succces((int)HttpStatusCode.OK, carModelDtos));
        }


        [ServiceFilter(typeof(NotFoundFilter<CarModelEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var carModelEntity = await _carModelService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<CarModelDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarModelDto>(carModelEntity)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CarModelDto carModelDto)
        {
            var carModelEntity = _mapper.Map<CarModelEntity>(carModelDto);
            var carModelResult = await _carModelService.AddAsync(carModelEntity);
            return CreateActionResult(CustomResponseDto<CarModelDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarModelDto>(carModelEntity)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CarModelDto carModelDto)
        {
            await _carModelService.UpdateAsync(_mapper.Map<CarModelEntity>(carModelDto));
            var carModelEntity = await _carModelService.GetByIdAsync((long)carModelDto.Id);
            return CreateActionResult(CustomResponseDto<CarModelDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarModelDto>(carModelEntity)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            var carModelEntity = await _carModelService.GetByIdAsync(id);
            await _carModelService.RemoveAsync(carModelEntity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        }
    }
}
