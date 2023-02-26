using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.Core.Services.MoorService;
using Moor.Model.Dtos.MoorDto.CarParameterDto;
using Moor.Model.Model.Authorize;
using Moor.Model.Utilities.Authentication;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

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
            var newPersonellModel = _authorizationService.Register(personnelModel).Result;
            return Ok(newPersonellModel);
        }
    }
}
