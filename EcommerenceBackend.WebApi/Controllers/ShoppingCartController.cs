using EcommerenceBackend.Application.Domain.Users;
using EcommerenceBackend.Application.Dto.Orders.Request;
using EcommerenceBackend.Application.Dto.ShoppingCart.Request;
using EcommerenceBackend.Application.UseCases.Orders.Queries.GetOrderById;
using EcommerenceBackend.Application.UseCases.Queries.GetUserProfileById;
using EcommerenceBackend.Application.UseCases.ShoppingCart.Commands;
using EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.AddToCart;
using EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.RemoveCartItem;
using EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.RemoveCartItem.RemoveCartItemByCartItemId;
using EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.RemoveCartItem.RemoveCartItemByShoppingCartId;
using EcommerenceBackend.Application.UseCases.ShoppingCart.Queries.GetCartItemsByCustomerId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcommerenceBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("addItem")]
        public async Task<IActionResult> AddToCart([FromBody] AddCartItemCommand command)
        {
            var cartId = await _mediator.Send(command);
            return Ok(new { CartId = cartId });
        }

        [HttpGet("getCartItemsByCustomerId")]
        public async Task<IActionResult> GetCartItemsByCustomerId([FromQuery] CartItemsByCustomerIdRequest request)
        {
            var query = new GetCartItemsByCustomerIdQuery { CustomerId = request.CustomerId!.Value };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpDelete("cartItemId")]
        public async Task<IActionResult> RemoveCartItem(Guid cartItemId)
        {
            var query = new RemoveCartItemCommand(cartItemId);
            var result = await _mediator.Send(query);
            return result ? Ok(new { message = "Item removed" }) : NotFound(new { message = "Item not found" });
        }
        [HttpPost("clearCart")]
        public async Task<IActionResult> ClearCart([FromQuery] Guid shoppingCartId)
        {
            var result = await _mediator.Send(new ClearCartCommand(shoppingCartId));
            return result ? Ok(new { message = "Cart cleared" }) : NotFound(new { message = "Cart not found" });
        }

    }
}
