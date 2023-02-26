using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Controllers
{
    [ValidateFilter]
    [HasPermission]
    public class AgenciesController : CustomBaseController
    {
        private readonly IAgencyService _agencyService;
        private readonly IMapper _mapper;

        public AgenciesController(IAgencyService agencyService, IMapper mapper)
        {
            _agencyService = agencyService;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<IActionResult> All()
        //{
        //    //AgencyDto
        //    //var cars = await _agencyService.GetAllAsync();
        //    //return CreateActionResult(CustomResponseDto<List<CarDto>>.Succces((int)HttpStatusCode.OK,));
        //}

        //[ServiceFilter(typeof(NotFoundFilter<CarEntity>))]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(long id)
        //{
        //    var car = await _agencyService.GetByIdAsync(id);
        //    return CreateActionResult(CustomResponseDto<CarDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CarDto>(car)));
        //    //return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.NotFound, string.Empty));
        //}

        //[HttpPost]
        //public async Task<IActionResult> Save(CarDto carDto)
        //{
        //    return CreateActionResult(CustomResponseDto<CarDto>.Succces((int)HttpStatusCode.OK, _agencyService.Save(carDto)));
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update(CarDto CarDto)
        //{
        //    return CreateActionResult(CustomResponseDto<CarDto>.Succces((int)HttpStatusCode.OK));
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Remove(long id)
        //{
        //    //var carEntity = await _carService.GetByIdAsync(id);
        //    //await _contentService.RemoveAsync(contentModel);
        //    //return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        //}
    }
}
