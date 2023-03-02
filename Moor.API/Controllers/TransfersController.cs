using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferPostDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Controllers
{
    [HasPermission]
    public class TransfersController : CustomBaseController
    {
        private readonly ITransferService _transferService;
        private readonly IMapper _mapper;

        public TransfersController(ITransferService transferService, IMapper mapper)
        {
            _transferService = transferService;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<IActionResult> All()
        //{
        //    var transferEntities = await _transferService.GetAllAsync();
        //    var cityDtos = _mapper.Map<List<CityDto>>(transferEntities);
        //    return CreateActionResult(CustomResponseDto<List<CityDto>>.Succces((int)HttpStatusCode.OK, cityDtos));
        //}

        //[ServiceFilter(typeof(NotFoundFilter<CityEntity>))]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(long id)
        //{
        //    var cityEntity = await _cityService.GetByIdAsync(id);
        //    return CreateActionResult(CustomResponseDto<CityDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<CityDto>(cityEntity)));
        //}

        [HttpPost]
        public async Task<IActionResult> Save(TransferPostDto transferPostDto)
        {
            var transferViewDto = await _transferService.CreateTransfer(transferPostDto);
            return CreateActionResult(CustomResponseDto<TransferViewDto>.Succces((int)HttpStatusCode.OK, transferViewDto));
        }

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
