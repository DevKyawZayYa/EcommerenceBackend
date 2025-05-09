using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Dto.Orders.Request;
using EcommerenceBackend.Application.Dto.Orders.Response;
using EcommerenceBackend.Application.UseCases.Orders.Commands.CreateOrder;
using EcommerenceBackend.Application.UseCases.Orders.Queries.GetOrderById;
using EcommerenceBackend.Application.UseCases.Orders.Queries.GetOrderListByCustomerId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerenceBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return Ok(CreatedAtAction(nameof(GetOrderById), new { id = orderId }, new { OrderId = orderId }));
        }

        [HttpPost("getOrderById")]
        public async Task<IActionResult> GetOrderById([FromBody] OrderRequest request)
        {
            var orderId = request.OrderId;
            var customerId = request.CustomerId;
            var order = await _mediator.Send(new GetOrderByIdQuery(orderId!, customerId!));
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost("getOrderListByCustomerId")]
        public async Task<IActionResult> getOrderListByCustomerId([FromBody] OrderRequest request)
        {
            var customerId = request.CustomerId;
            var order = await _mediator.Send(new GetOrderListByCustomerIdQuery( customerId!));
            if (order == null) return NotFound();
            return Ok(order);
        }
    }

}
