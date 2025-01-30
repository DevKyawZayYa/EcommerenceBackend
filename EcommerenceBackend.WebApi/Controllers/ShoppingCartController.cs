using EcommerenceBackend.Application.Dto.ShoppingCart.Request;
using EcommerenceBackend.Application.UseCases.ShoppingCart.Commands;
using EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.AddToCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcommerenceBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            var cartId = await _mediator.Send(command);
            return Ok(new { CartId = cartId });
        }
    }
}
