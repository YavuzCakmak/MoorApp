using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Extension.String;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Core.Utilities.DataFilter;
using Moor.Model.Dtos.MoorDto.AgencyDto;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Dtos.MoorDto.DriverDto;
using Moor.Model.Models.MoorModels.AgencyModel;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Model.Utilities;
using Moor.Service.Models.Dto.ResponseDto;
using Moor.Service.Services.MoorService;
using System.Net;

namespace Moor.API.Controllers
{
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
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var agencyEntities = await _agencyService.GetAllAsync(dataFilterModel);
            var agencyDtos = _mapper.Map<List<AgencyDto>>(agencyEntities);
            return CreateActionResult(CustomResponseDto<List<AgencyDto>>.Succces((int)HttpStatusCode.OK, agencyDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<AgencyEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var agencyEntity = await _agencyService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<AgencyDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<AgencyDto>(agencyEntity)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(AgencyModel agencyModel)
        {
            var agencyPostResult = await _agencyService.Save(agencyModel);
            if (agencyPostResult.IsSuccess)
            {
                var agencyEntity = _agencyService.Where(x => x.Id == agencyPostResult.PkId).FirstOrDefault();
                var agencyDto = _mapper.Map<AgencyDto>(agencyEntity);
                return CreateActionResult(CustomResponseDto<AgencyDto>.Succces((int)HttpStatusCode.OK, agencyDto));
            }
            else
            {
                if (agencyPostResult.ErrorMessage.IsNotNullOrEmpty())
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, agencyPostResult.ErrorMessage));
                }
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(CarDto CarDto)
        {
            return CreateActionResult(CustomResponseDto<CarDto>.Succces((int)HttpStatusCode.OK));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            var agencyEntity = await _agencyService.GetByIdAsync(id);
            await _agencyService.RemoveAsync(agencyEntity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        }
    }
}
