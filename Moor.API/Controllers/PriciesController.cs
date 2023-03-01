using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Model.Dtos.MoorDto.CityDto;
using Moor.Model.Dtos.MoorDto.PriceDto;
using Moor.Model.Dtos.MoorDto.PriceDto.GetPriceWithCarParameterIdAndDistrictIdDto;
using Moor.Model.Models.MoorModels.CityModel;
using Moor.Model.Models.MoorModels.PriceModel;
using Moor.Service.Models.Dto.ResponseDto;
using Moor.Service.Services.MoorService;
using System.Net;

namespace Moor.API.Controllers
{
    [HasPermission]
    public class PriciesController : CustomBaseController
    {
        private readonly IPriceService _priceService;
        private readonly IMapper _mapper;

        public PriciesController(IPriceService priceService, IMapper mapper)
        {
            _priceService = priceService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var priceEntities = await _priceService.GetAllAsync();
            var priceDtos = _mapper.Map<List<PriceDto>>(priceEntities);
            return CreateActionResult(CustomResponseDto<List<PriceDto>>.Succces((int)HttpStatusCode.OK, priceDtos));
        }


        [HttpPost("GetPriceWithCarParameterAndDistrictId")]
        public async Task<IActionResult> GetPriceWithCarParameterAndDistrictId([FromBody] GetPriceWithCarParameterAndDistrictDto getPriceWithCarParameterAndDistrictDto)
        {
            var priceEntities = _priceService.Where(x=> x.CarParameterId == getPriceWithCarParameterAndDistrictDto.CarParameterId && x.DistrictId == getPriceWithCarParameterAndDistrictDto.DistrictId).FirstOrDefault();
            var priceDtos = _mapper.Map<PriceDto>(priceEntities);
            return CreateActionResult(CustomResponseDto<PriceDto>.Succces((int)HttpStatusCode.OK, priceDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<PriceEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var priceEntity = await _priceService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<PriceDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<PriceDto>(priceEntity)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(PriceModel priceModel)
        {
            var priceEntity = _mapper.Map<PriceEntity>(priceModel);
            return CreateActionResult(CustomResponseDto<PriceDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<PriceDto>(await _priceService.AddAsync(priceEntity))));
        }

        [HttpPut]
        public async Task<IActionResult> Update(PriceModel priceModel)
        {
            await _priceService.UpdateAsync(_mapper.Map<PriceEntity>(priceModel));
            return CreateActionResult(CustomResponseDto<PriceDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<PriceDto>(_priceService.GetByIdAsync((long)priceModel.Id))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            var priceEntity = await _priceService.GetByIdAsync(id);
            await _priceService.RemoveAsync(priceEntity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        }
    }
}

