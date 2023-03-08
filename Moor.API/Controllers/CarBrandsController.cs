using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Core.Utilities.DataFilter;
using Moor.Model.Dtos.MoorDto.CarBrandDto;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Dtos.MoorDto.CarModelDto;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Service.Models.Dto.ResponseDto;
using Moor.Service.Services.MoorService;
using System.Net;

namespace Moor.API.Controllers
{
    [HasPermission]
    public class CarBrandsController : CustomBaseController
    {
        private readonly ICarBrandService _carBrandService;
        private readonly IMapper _mapper;

        public CarBrandsController(ICarBrandService carBrandService, IMapper mapper)
        {
            _carBrandService = carBrandService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var carBrandEntities = await _carBrandService.GetAllAsync(dataFilterModel);
            var carBrandDtos = _mapper.Map<List<CarBrandDto>>(carBrandEntities);
            return CreateActionResult(CustomResponseDto<List<CarBrandDto>>.Succces((int)HttpStatusCode.OK, carBrandDtos));
        }


        [ServiceFilter(typeof(NotFoundFilter<CarBrandEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var carBrandEntity = await _carBrandService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<CarBrandDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarBrandDto>(carBrandEntity)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CarBrandDto carBrandDto)
        {
            var carBrandEntity = _mapper.Map<CarBrandEntity>(carBrandDto);
            var carBrandResult = await _carBrandService.AddAsync(carBrandEntity);
            return CreateActionResult(CustomResponseDto<CarBrandDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarBrandDto>(carBrandResult)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CarBrandDto carBrandDto)
        {
            await _carBrandService.UpdateAsync(_mapper.Map<CarBrandEntity>(carBrandDto));
            var carBrandEntity = await _carBrandService.GetByIdAsync((long)carBrandDto.Id);
            return CreateActionResult(CustomResponseDto<CarBrandDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarBrandDto>(carBrandEntity)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            var carBrandEntity = await _carBrandService.GetByIdAsync(id);
            await _carBrandService.RemoveAsync(carBrandEntity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        }
    }
}
