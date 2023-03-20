using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Core.Utilities.DataFilter;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Dtos.MoorDto.DistrictDto;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Model.Models.MoorModels.DistrictModel;
using Moor.Service.Models.Dto.ResponseDto;
using Moor.Service.Services.MoorService;
using System.Net;

namespace Moor.API.Controllers
{
    //[HasPermission]
    public class DistrictiesController : CustomBaseController
    {
        private readonly IDistrictService _districtService;
        private readonly IMapper _mapper;

        public DistrictiesController(IDistrictService districtService, IMapper mapper)
        {
            _districtService = districtService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var districtEntities = await _districtService.GetAllAsync(dataFilterModel);
            var districtModels = _mapper.Map<List<DisctrictDto>>(districtEntities);
            return CreateActionResult(CustomResponseDto<List<DisctrictDto>>.Succces((int)HttpStatusCode.OK, districtModels));
        }


        [ServiceFilter(typeof(NotFoundFilter<DistrictEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var districtEntity = await _districtService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<DisctrictDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<DisctrictDto>(districtEntity)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(DistrictModel districtModel)
        {
            var districtEntity = _mapper.Map<DistrictEntity>(districtModel);
            return CreateActionResult(CustomResponseDto<DistrictModel>.Succces((int)HttpStatusCode.OK, _mapper.Map<DistrictModel>(await _districtService.AddAsync(districtEntity))));
        }

        [HttpPut]
        public async Task<IActionResult> Update(DistrictModel districtModel)
        {
            await _districtService.UpdateAsync(_mapper.Map<DistrictEntity>(districtModel));
            return CreateActionResult(CustomResponseDto<DistrictModel>.Succces((int)HttpStatusCode.OK, _mapper.Map<DistrictModel>(_districtService.GetByIdAsync((long)districtModel.Id))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            var districtEntity = await _districtService.GetByIdAsync(id);
            await _districtService.RemoveAsync(districtEntity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        }
    }
}
