using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.Core.Services.MoorService;
using Moor.Model.Model.Authorize;
using Moor.Model.Utilities.Authentication;

namespace Moor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizeService _authorizationService;

        public AuthorizationController(IAuthorizeService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            return Ok(_authorizationService.Login(loginModel));
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] PersonnelModel personnelModel)
        {
            return Ok(_authorizationService.Register(personnelModel));
        }
    }
}
