using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Model.Dtos.MoorDto;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Controllers
{
    [HasPermission]
    public class TravellersController : CustomBaseController
    {
        private readonly ITravellerService _travellerService;
        private readonly IMapper _mapper;

        public TravellersController(ITravellerService travellerService, IMapper mapper)
        {
            _travellerService = travellerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var travellerEntities = await _travellerService.GetAllAsync();
            var travellerDtos = _mapper.Map<List<TravellerDto>>(travellerEntities);
            return CreateActionResult(CustomResponseDto<List<TravellerDto>>.Succces((int)HttpStatusCode.OK, travellerDtos));
        }

        //[ServiceFilter(typeof(NotFoundFilter<CityEntity>))]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(long id)
        //{
        //    var cityEntity = await _cityService.GetByIdAsync(id);
        //    return CreateActionResult(CustomResponseDto<CityDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CityDto>(cityEntity)));
        //}

        //[HttpPost]
        //public async Task<IActionResult> Save(CityModel cityModel)
        //{
        //    var cityEntity = _mapper.Map<CityEntity>(cityModel);
        //    return CreateActionResult(CustomResponseDto<CityDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CityDto>(await _cityService.AddAsync(cityEntity))));
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update(CityModel cityModel)
        //{
        //    await _cityService.UpdateAsync(_mapper.Map<CityEntity>(cityModel));
        //    return CreateActionResult(CustomResponseDto<CityDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CityDto>(_cityService.GetByIdAsync((long)cityModel.Id))));
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Remove(long id)
        //{
        //    var cityEntiy = await _cityService.GetByIdAsync(id);
        //    await _cityService.RemoveAsync(cityEntiy);
        //    return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        //}
    }
}
