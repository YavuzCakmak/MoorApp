using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.Service.Models.Dto.ResponseDto;

namespace Moor.API.Controllers.BaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        //EndPoint Olmadığı için eklenir. Eklenmez ise Get-Post olmadığı için hata verir. 
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode,
                };

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
