using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;
using Moor.Core.Utilities.DataFilter;
using Moor.Model.Dtos.MoorDto.CarParameterDto;
using Moor.Model.Models.MoorModels.CarParameterModel;
using Moor.Model.Models.MoorModels.NotificationModel;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Controllers
{
    [HasPermission]
    public class NotificationsController : CustomBaseController
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationsController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var notificationEntities = await _notificationService.GetAllAsync(dataFilterModel);
            return CreateActionResult(CustomResponseDto<List<NotificationModel>>.Succces((int)HttpStatusCode.OK, _mapper.Map<List<NotificationModel>>(notificationEntities)));
        }

        [ServiceFilter(typeof(NotFoundFilter<NotificationEntity>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var notificationEntity = await _notificationService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<NotificationModel>.Succces((int)HttpStatusCode.OK, _mapper.Map<NotificationModel>(notificationEntity)));
        }
    }
}
