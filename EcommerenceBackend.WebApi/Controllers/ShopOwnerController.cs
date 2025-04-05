using EcommerenceBackend.Application.UseCases.Shops.Commands.CreateShopOwner;
using EcommerenceBackend.Application.UseCases.Shops.Queries.GetShopOwnerByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerenceBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ShopOwnerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopOwnerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShopOwner([FromBody] CreateShopOwnerCommand command)
        {
            var shopOwnerId = await _mediator.Send(command);
            return Ok(CreatedAtAction(nameof(CreateShopOwner), new { id = shopOwnerId }, shopOwnerId));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShopOwnerById(Guid id)
        {
            var shopOwner = await _mediator.Send(new GetShopOwnerByIdQuery(id));
            return Ok(shopOwner);
        }
    }
}
