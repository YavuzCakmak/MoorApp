using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Core.Utilities.DataFilter;
using Moor.Model.Dtos.MoorDto.CityDto;
using Moor.Model.Models.MoorModels.CityModel;
using Moor.Model.Models.MoorModels.CountryModel;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Controllers
{
    public class CitiesController : CustomBaseController
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CitiesController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var cityEntities = await _cityService.GetAllAsync(dataFilterModel);
            var cityDtos = _mapper.Map<List<CityDto>>(cityEntities);
            return CreateActionResult(CustomResponseDto<List<CityDto>>.Succces((int)HttpStatusCode.OK, cityDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<CityEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var cityEntity = await _cityService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<CityDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CityDto>(cityEntity)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CityModel cityModel)
        {
            var cityEntity = _mapper.Map<CityEntity>(cityModel);
            return CreateActionResult(CustomResponseDto<CityDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CityDto>(await _cityService.AddAsync(cityEntity))));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CityModel cityModel)
        {
            await _cityService.UpdateAsync(_mapper.Map<CityEntity>(cityModel));
            return CreateActionResult(CustomResponseDto<CityDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CityDto>(_cityService.GetByIdAsync((long)cityModel.Id))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            var cityEntiy = await _cityService.GetByIdAsync(id);
            await _cityService.RemoveAsync(cityEntiy);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        }
    }
}
