using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferPostDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto;
using Moor.Model.Models.MoorModels.TransferModel;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Controllers
{
    //[HasPermission]
    public class TransfersController : CustomBaseController
    {
        private readonly ITransferService _transferService;
        private readonly IMapper _mapper;

        public TransfersController(ITransferService transferService, IMapper mapper)
        {
            _transferService = transferService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var transferEntities = await _transferService.GetAllAsync();
            var transferViewDtos = _mapper.Map<List<TransferViewDto>>(transferEntities);
            return CreateActionResult(CustomResponseDto<List<TransferViewDto>>.Succces((int)HttpStatusCode.OK, transferViewDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<TransferEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var transferEntity = await _transferService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<TransferViewDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<TransferViewDto>(transferEntity)));
        }

        [HttpPost]
        public async Task<IActionResult> Save(TransferPostDto transferPostDto)
        {
            var transferViewDto = await _transferService.CreateTransfer(transferPostDto);
            return CreateActionResult(CustomResponseDto<TransferViewDto>.Succces((int)HttpStatusCode.OK, transferViewDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(TransferModel transferModel)
        {
            await _transferService.UpdateAsync(_mapper.Map<TransferEntity>(transferModel));
            return CreateActionResult(CustomResponseDto<TransferViewDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<TransferViewDto>(_transferService.GetByIdAsync((long)transferModel.Id))));
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Remove(long id)
        //{
        //    var cityEntiy = await _cityService.GetByIdAsync(id);
        //    await _cityService.RemoveAsync(cityEntiy);
        //    return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        //}
    }
}
