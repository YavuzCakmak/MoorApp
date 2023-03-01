using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using Moor.Model.Models.MoorModels.CityModel;
using Moor.Model.Models.MoorModels.CountryModel;
using Moor.Model.Models.MoorModels.CountyModel;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Controllers
{
    public class CountiesController : CustomBaseController
    {
        private readonly ICountyService _countyService;
        private readonly IMapper _mapper;

        public CountiesController(ICountyService countyService, IMapper mapper)
        {
            _countyService = countyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var countyEntities = await _countyService.GetAllAsync();
            var countyModels = _mapper.Map<List<CountyModel>>(countyEntities);
            return CreateActionResult(CustomResponseDto<List<CountyModel>>.Succces((int)HttpStatusCode.OK, countyModels));
        }

        [ServiceFilter(typeof(NotFoundFilter<CountyEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var countyEntity = await _countyService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<CountyModel>.Succces((int)HttpStatusCode.OK, _mapper.Map<CountyModel>(countyEntity)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CountyModel countyModel)
        {
            var countyEntity = _mapper.Map<CountyEntity>(countyModel);
            return CreateActionResult(CustomResponseDto<CountyModel>.Succces((int)HttpStatusCode.OK, _mapper.Map<CountyModel>(await _countyService.AddAsync(countyEntity))));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CountyModel countyModel)
        {
            await _countyService.UpdateAsync(_mapper.Map<CountyEntity>(countyModel));
            return CreateActionResult(CustomResponseDto<CityModel>.Succces((int)HttpStatusCode.OK, _mapper.Map<CityModel>(_countyService.GetByIdAsync((long)countyModel.Id))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            var countyEntity = await _countyService.GetByIdAsync(id);
            await _countyService.RemoveAsync(countyEntity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        }
    }
}
