using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities;

namespace Moor.API.Controllers
{
    [ValidateFilter]
    [HasPermission]
    public class CarParametersController : CustomBaseController
    {
        private readonly ICarParameterService _carParameterService;
        private readonly IMapper _mapper;

        public CarParametersController(ICarParameterService carParameterService, IMapper mapper)
        {
            _carParameterService = carParameterService;
            _mapper = mapper;
        }

        //Constuructor

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var cars = await _carParameterService.GetAllAsync();
            return Ok(cars);
        }
    }
}
