using EcommerenceBackend.Application.UseCases.Shops.Commands.CreateShop;
using EcommerenceBackend.Application.UseCases.Shops.Queries.GetShopByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerenceBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ShopController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShopById(Guid id)
        {
            var shop = await _mediator.Send(new GetShopByIdQuery(id));
            return Ok(shop);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShop([FromBody] CreateShopCommand command)
        {
            var shopId = await _mediator.Send(command);
            return Ok(CreatedAtAction(nameof(GetShopById), new { id = shopId }, shopId));
        }
    }
}
