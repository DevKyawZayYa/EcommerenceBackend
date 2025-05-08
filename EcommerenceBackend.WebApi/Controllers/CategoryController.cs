using EcommerenceBackend.Application.Dto.Categories;
using EcommerenceBackend.Application.UseCases.Categories.Commands.CreateCategory;
using EcommerenceBackend.Application.UseCases.Categories.Queries.GetCategoryList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerenceBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto createCategoryDto)
        {
            var categoryId = await _mediator.Send(new CreateCategoryCommand(createCategoryDto));
            return Ok(categoryId);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryList()
        {
            var categories = await _mediator.Send(new GetCategoryListQuery());
            return Ok(categories);
        }
    }
}
