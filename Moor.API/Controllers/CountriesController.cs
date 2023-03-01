using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Models.MoorModels.CountryModel;
using Moor.Model.Models.MoorModels.DistrictModel;
using Moor.Service.Models.Dto.ResponseDto;
using Moor.Service.Services.MoorService;
using System.Net;

namespace Moor.API.Controllers
{
    [HasPermission]
    public class CountriesController : CustomBaseController
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountriesController(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var countriesEntities = await _countryService.GetAllAsync();
            var countriesModels = _mapper.Map<List<CountryModel>>(countriesEntities);
            return CreateActionResult(CustomResponseDto<List<CountryModel>>.Succces((int)HttpStatusCode.OK, countriesModels));
        }


        [ServiceFilter(typeof(NotFoundFilter<CountryEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var countryEntity = await _countryService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<CountryModel>.Succces((int)HttpStatusCode.OK, _mapper.Map<CountryModel>(countryEntity)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CountryModel countryModel)
        {
            var countryEntity = _mapper.Map<CountryEntity>(countryModel);
            return CreateActionResult(CustomResponseDto<CountryModel>.Succces((int)HttpStatusCode.OK, _mapper.Map<CountryModel>(await _countryService.AddAsync(countryEntity))));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CountryModel countryModel)
        {
            await _countryService.UpdateAsync(_mapper.Map<CountryEntity>(countryModel));
            return CreateActionResult(CustomResponseDto<CountryModel>.Succces((int)HttpStatusCode.OK, _mapper.Map<CountryModel>(_countryService.GetByIdAsync((long)countryModel.Id))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            var countryEntity = await _countryService.GetByIdAsync(id);
            await _countryService.RemoveAsync(countryEntity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        }
    }
}