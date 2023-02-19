using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.API.Filters;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using System.Net;

namespace Moor.API.Controllers
{
    [ValidateFilter]
    public class CarsController : CustomBaseController
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarsController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        //Constuructor

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var cars = await _carService.GetAllAsync();
            return Ok(cars);
            //var contents = await _contentService.GetAllAsync();
            //var contentDtos = _mapper.Map<List<ContentDto>>(contents.ToList());
            //return CreateActionResult(CustomResponseDto<List<ContentDto>>.Succces((int)HttpStatusCode.OK, contentDtos));
        }


        //[ServiceFilter(typeof(NotFoundFilter<CarEntity>))]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    //var content = await _contentService.GetByIdAsync(id);
        //    //var contentDto = _mapper.Map<ContentDto>(content);
        //    //return CreateActionResult(CustomResponseDto<ContentDto>.Succces((int)HttpStatusCode.OK, contentDto));
        //}

        //[HttpPost]
        //public async Task<IActionResult> Save(ContentDto contentDto)
        //{
        //    //var content = await _contentService.AddAsync(_mapper.Map<Content>(contentDto));
        //    //return CreateActionResult(CustomResponseDto<ContentDto>.Succces((int)HttpStatusCode.OK, _mapper.Map<ContentDto>(content)));
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update(ContentDto contentDto)
        //{
        //    await _contentService.UpdateAsync(_mapper.Map<Content>(contentDto));
        //    return CreateActionResult(CustomResponseDto<ContentDto>.Succces((int)HttpStatusCode.OK));
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Remove(int id)
        //{
        //    var contentModel = await _contentService.GetByIdAsync(id);
        //    await _contentService.RemoveAsync(contentModel);
        //    return CreateActionResult(CustomResponseDto<NoContentDto>.Succces((int)HttpStatusCode.OK));
        //}

    }
}
