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
using Moor.Model.Dtos.MoorDto.TransferDto.TransferPostDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto;
using Moor.Model.Models.MoorModels.AgencyModel.AgencyWalletModel;
using Moor.Model.Models.MoorModels.AgencyModel.DebitForAgencyModel;
using Moor.Model.Models.MoorModels.DriverModel.DebitForDriverModel;
using Moor.Model.Models.MoorModels.DriverModel.DriverWalletModel;
using Moor.Model.Models.MoorModels.TransferModel;
using Moor.Model.Models.MoorModels.TransferModel.GetTransferUpdateModel;
using Moor.Model.Models.MoorModels.TransferModel.TransferChangeModel;
using Moor.Model.Models.MoorModels.TransferModel.TransferGetByIdModel;
using Moor.Model.Models.MoorModels.TransferModel.TransferUpdateModel;
using Moor.Model.Utilities;
using Moor.Service.Models.Dto.ResponseDto;
using Moor.Service.Services.MoorService;
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
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var transferEntities = await _transferService.GetAllAsync(dataFilterModel);
            var transferViewDtos = await _transferService.MapTransferViewDtos(transferEntities.ToList());
            return CreateActionResult(CustomResponseDto<List<TransferViewDto>>.Succces((int)HttpStatusCode.OK, transferViewDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<TransferEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var transferEntity = await _transferService.GetByIdAsync(id);
            var transferViewModel = await _transferService.MapTransferViewDto(transferEntity);
            return CreateActionResult(CustomResponseDto<TransferViewDto>.Succces((int)HttpStatusCode.OK, transferViewModel));
        }

        [HttpGet("GetTransferUpdateModel")]
        public async Task<IActionResult> GetTransferUpdateModel([FromQuery]long transferId)
        {
            var getTransferUpdateModel = await _transferService.GetTransferUpdateModel(transferId);
            if (getTransferUpdateModel.IsNotNull())
            {
                return CreateActionResult(CustomResponseDto<GetTransferUpdateModel>.Succces((int)HttpStatusCode.OK, getTransferUpdateModel));
            }
            else
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest));
        }

        [HttpGet("GetTransferDetail")]
        public async Task<IActionResult> GetTransferDetail([FromQuery] long transferId)
        {
            var transferGetByIdModel = _transferService.GetTransferDetail(transferId).Result;
            return CreateActionResult(CustomResponseDto<TransferGetByIdModel>.Succces((int)HttpStatusCode.OK, transferGetByIdModel));
        }

        [HttpGet("GetDriverWallet")]
        public async Task<IActionResult> GetDriverWallet([FromQuery] long driverId)
        {
            var driverWallet = _transferService.GetDriverWallet(driverId).Result;
            return CreateActionResult(CustomResponseDto<DriverWalletModel>.Succces((int)HttpStatusCode.OK, driverWallet));
        }

        [HttpGet("GetAgencyWallet")]
        public async Task<IActionResult> GetAgencyWallet([FromQuery] long agencyId)
        {
            var agencyWallet = _transferService.GetAgencyWallet(agencyId).Result;
            return CreateActionResult(CustomResponseDto<AgencyWalletModel>.Succces((int)HttpStatusCode.OK, agencyWallet));
        }

        [HttpPost("AddDebitForDriver")]
        public async Task<IActionResult> AddDebitForDriver([FromBody] DebitForDriverModel debitForDriverModel)
        {
            var dataResult = await _transferService.AddDebitForDriver(debitForDriverModel);
            if (dataResult.IsSuccess)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
            else
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, dataResult.ErrorMessage));
        }

        [HttpPost("AddDebitForAgency")]
        public async Task<IActionResult> AddDebitForAgency([FromBody] DebitForAgencyModel debitForAgencyModel)
        {
            var dataResult = await _transferService.AddDebitForAgency(debitForAgencyModel);
            if (dataResult.IsSuccess)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
            else
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, dataResult.ErrorMessage));
        }


        [HttpPost("ChangeTransferStatus")]
        public async Task<IActionResult> ChangeTransferStatus([FromBody] TransferChangeModel transferChangeModel)
        {
            var dataResult = await _transferService.ChangeTransferStatus(transferChangeModel);
            if (dataResult.IsSuccess)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
            else
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, dataResult.ErrorMessage));
        }

        [HttpPost]
        public async Task<IActionResult> Save(TransferPostDto transferPostDto)
        {
            var dataResult = await _transferService.CreateTransfer(transferPostDto);
            if (dataResult.IsSuccess)
            {
                var transferEntity = _transferService.Where(x => x.Id == dataResult.PkId).FirstOrDefault();
                var transferViewDto = await _transferService.MapTransferViewDto(transferEntity);
                return CreateActionResult(CustomResponseDto<TransferViewDto>.Succces((int)HttpStatusCode.OK, transferViewDto));
            }
            else
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, dataResult.ErrorMessage));

        }

        [HttpPut("UpdateTransfer")]
        public async Task<IActionResult> UpdateTransfer(TransferUpdateModel transferUpdateModel)
        {
            var dataResult = await _transferService.UpdateTransfer(transferUpdateModel);
            if (dataResult.IsSuccess)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
            else
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.BadRequest, dataResult.ErrorMessage));
        }

        [HttpPut]
        public async Task<IActionResult> Update(TransferModel transferModel)
        {
            await _transferService.UpdateAsync(_mapper.Map<TransferEntity>(transferModel));
            return CreateActionResult(CustomResponseDto<TransferViewDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<TransferViewDto>(_transferService.GetByIdAsync((long)transferModel.Id))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            var transferEntity = await _transferService.GetByIdAsync(id);
            await _transferService.RemoveAsync(transferEntity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        }
    }
}
