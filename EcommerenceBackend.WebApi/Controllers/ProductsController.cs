// EcommerenceBackend.WebApi/Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.UseCases.Products.Commands.CreateProduct;
using EcommerenceBackend.Application.UseCases.Products.Queries.GetProductDetailsById;
using EcommerenceBackend.Application.UseCases.Products.Commands.UpdateProduct;
using EcommerenceBackend.Application.UseCases.Products.Queries.GetAllProducts;
using EcommerenceBackend.Application.UseCases.Products.Commands.DeleteProduct;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using Microsoft.AspNetCore.Authorization;
using EcommerenceBackend.Application.UseCases.Products.Queries.GetProductListByCategoryId;
using EcommerenceBackend.Application.UseCases.Products.Queries.SearchProduct;

namespace EcommerenceBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            var productId = await _mediator.Send(new CreateProductCommand(createProductDto));
            return Ok(productId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailsById(Guid id)
        {
            var productId = new ProductId(id);
            var productDetails = await _mediator.Send(new GetProductDetailsByIdQuery(productId));
            return Ok(productDetails);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductCommand command)
        {
            command.ProductId = new ProductId(id);
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllProductsQuery {Page = page, PageSize = pageSize };
            var products = await _mediator.Send(query);
            return Ok(products);
        }
        [HttpGet("category/{CategoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(Guid CategoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetProductListByCategoryIdQuery { CategoryId = CategoryId, Page = page, PageSize = pageSize };
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var productId = new ProductId(id);
            var command = new DeleteProductCommand { ProductId = productId };
            var result = await _mediator.Send(command);
            return Ok(result ? NoContent() : NotFound());
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] SearchProductQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
