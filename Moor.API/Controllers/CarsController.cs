using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Model.Authorize;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Controllers
{
    [ValidateFilter]
    [HasPermission]
    public class CarsController : CustomBaseController
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarsController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var cars = await _carService.GetAllAsync();
            var carModels = _mapper.Map<List<CarModel>>(cars);
            var carDtos = _mapper.Map<List<CarDto>>(carModels);
            return CreateActionResult(CustomResponseDto<List<CarDto>>.Succces((int)HttpStatusCode.OK, carDtos));
        }


        [ServiceFilter(typeof(NotFoundFilter<CarEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var car = await _carService.GetByIdAsync(id);
            var carModel = _mapper.Map<CarModel>(car);
            var carEn = _mapper.Map<CarDto>(car);
            return CreateActionResult(CustomResponseDto<CarDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarDto>(carModel)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CarDto carDto)
        {
            var carEntity = await _carService.AddAsync(_mapper.Map<CarEntity>(carDto));
            return CreateActionResult(CustomResponseDto<CarDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarDto>(carEntity)));
        }

        //[HttpPut]
        //public async Task<IActionResult> Update(ContentDto contentDto)
        //{
        //    await _contentService.UpdateAsync(_mapper.Map<Content>(contentDto));
        //    return CreateActionResult(CustomResponseDto<ContentDto>.Succces((int)HttpStatusCode.OK));
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Remove(int id)
        //{
        //    var contentModel = await _contentService.GetByIdAsync(id);
        //    await _contentService.RemoveAsync(contentModel);
        //    return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        //}

    }
}
