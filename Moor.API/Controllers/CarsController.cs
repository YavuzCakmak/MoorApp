using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.Base;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Extension.String;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Core.Utilities.DataFilter;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Model.Authorize;
using Moor.Model.Models.MoorModels.Car;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Model.Utilities;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Controllers
{
    //[HasPermission]
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
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var carEntities = await _carService.GetAllAsync(dataFilterModel);
            var carDtos = _mapper.Map<List<CarDto>>(carEntities);
            return CreateActionResult(CustomResponseDto<List<CarDto>>.Succces((int)HttpStatusCode.OK, carDtos));
        }


        [ServiceFilter(typeof(NotFoundFilter<CarEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var carEntity = await _carService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<CarDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarDto>(carEntity)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CarPostModel carPostModel)
        {
            var dataResult = await _carService.Save(carPostModel);
            if (dataResult.IsSuccess)
            {
                var carEntity = _carService.Where(x => x.Id == dataResult.PkId).FirstOrDefault();
                var carDto = _mapper.Map<CarDto>(carEntity);
                return CreateActionResult(CustomResponseDto<CarDto>.Succces((int)HttpStatusCode.OK, carDto));
            }
            else
            {
                if (dataResult.ErrorMessage.IsNotNullOrEmpty())
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, dataResult.ErrorMessage));
                }
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(CarPostModel carPostModel)
        {
            var carUpdateResult = await _carService.Update(carPostModel);
            if (carUpdateResult.IsSuccess)
            {
                var carEntityModel = _carService.Where(x => x.Id == carPostModel.Id).FirstOrDefault();
                var carDto = _mapper.Map<CarDto>(carEntityModel);
                return CreateActionResult(CustomResponseDto<CarDto>.Succces((int)HttpStatusCode.OK, carDto));
            }
            else
            {
                if (carUpdateResult.ErrorMessage.IsNotNullOrEmpty())
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, carUpdateResult.ErrorMessage));
                }
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            var carEntity = await _carService.GetByIdAsync(id);
            await _carService.RemoveAsync(carEntity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        }
    }
}
