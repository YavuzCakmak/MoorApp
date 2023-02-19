using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Moor.Core.Entities.Base;
using Moor.Core.Services.BaseService;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
            }

            var anyEntity = await _service.AnyAsync(x => x.Id == (int)idValue);
            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.NotFound, $"{typeof(T).Name}({idValue}) not found."));

        }
    }
}
