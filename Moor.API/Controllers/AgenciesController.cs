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
using Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto;
using Moor.Model.Model.Authorize;
using Moor.Model.Models.MoorModels.AgencyModel;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Model.Models.MoorModels.DriverModel;
using Moor.Model.Utilities;
using Moor.Service.Models.Dto.ResponseDto;
using Moor.Service.Services.MoorService;
using System.Net;

namespace Moor.API.Controllers
{
    //[HasPermission]
    public class AgenciesController : CustomBaseController
    {
        private readonly IAgencyService _agencyService;
        private readonly IPersonnelService _personnelService;
        private readonly ITransferService _transferService;
        private readonly IMapper _mapper;

        public AgenciesController(IAgencyService agencyService, IMapper mapper, IPersonnelService personnelService, ITransferService transferService)
        {
            _agencyService = agencyService;
            _mapper = mapper;
            _personnelService = personnelService;
            _transferService = transferService;
        }
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var agencyEntities = await _agencyService.GetAllAsync(dataFilterModel);
            var agencyDtos = _mapper.Map<List<AgencyDto>>(agencyEntities);
            foreach (var agencyDto in agencyDtos)
            {
                using (FileStream stream = new FileStream(agencyDto.MediaPath, FileMode.Open))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    string base64Data = Convert.ToBase64String(bytes);
                    agencyDto.MediaPath = base64Data;
                }
                var agencyTotalPrice = _transferService.Where(x => x.AgencyId == agencyDto.Id).Sum(x => x.AgencyAmount);
                agencyDto.AgencyTotalPrice = agencyTotalPrice;
            }
            return CreateActionResult(CustomResponseDto<List<AgencyDto>>.Succces((int)HttpStatusCode.OK, agencyDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<AgencyEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            #region Objects 
            AgencyModel agencyModel = new AgencyModel();
            PersonnelModel personnelModel = new PersonnelModel();
            #endregion

            var agencyEntity = await _agencyService.GetByIdAsync(id);
            if (agencyEntity.IsNotNull())
            {
                agencyModel.Id = agencyEntity.Id;
                agencyModel.AgencyName = agencyEntity.Name;
                agencyModel.AgencyEmail = agencyEntity.Email;
                agencyModel.Title = agencyEntity.Title;
                agencyModel.TaxOffice = agencyEntity.TaxOffice;
                agencyModel.TaxNumber = agencyEntity.TaxNumber;
                agencyModel.AgencyPhoneNumber = agencyEntity.PhoneNumber;
                agencyModel.OperationPhoneNumber = agencyEntity.OperationPhoneNumber;
                agencyModel.AgencyMediaPath = agencyEntity.MediaPath;
                agencyModel.AgencyDetails = agencyEntity.Details;
                agencyModel.ReceptionPrice = agencyEntity.ReceptionPrice;
                agencyModel.CityId = agencyEntity.CityId;
                agencyModel.CountyId = agencyEntity.CountyId;
            }
            return CreateActionResult(CustomResponseDto<AgencyModel>.Succces((int)HttpStatusCode.OK, agencyModel));
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
        public async Task<IActionResult> Update(AgencyModel agencyModel)
        {
            #region Objects 
            #endregion

            var agencyEntity = await _agencyService.GetByIdAsync(agencyModel.Id);
            agencyEntity.Id = agencyModel.Id;
            agencyEntity.Name = agencyModel.AgencyName;
            agencyEntity.Email = agencyModel.AgencyEmail;
            agencyEntity.Title = agencyModel.Title;
            agencyEntity.TaxOffice = agencyModel.TaxOffice;
            agencyEntity.TaxNumber = agencyModel.TaxNumber;
            agencyEntity.PhoneNumber = agencyModel.AgencyPhoneNumber;
            agencyEntity.OperationPhoneNumber = agencyModel.OperationPhoneNumber;
            agencyEntity.MediaPath = agencyModel.AgencyMediaPath;
            agencyEntity.Details = agencyModel.AgencyDetails;
            agencyEntity.ReceptionPrice = (decimal)agencyModel.ReceptionPrice;
            agencyEntity.CityId = agencyModel.CityId;
            agencyEntity.CountyId = agencyModel.CountyId;
            await _agencyService.UpdateAsync(agencyEntity);
            return CreateActionResult(CustomResponseDto<AgencyModel>.Succces((int)HttpStatusCode.OK));
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
