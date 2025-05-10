using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Dto.Orders.Response;
using EcommerenceBackend.Application.Dto.ShoppingCart.Response;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Application.UseCases.Orders.Commands.CreateOrder;
using EcommerenceBackend.Application.UseCases.Payments.Commands.CreatePayment;
using EcommerenceBackend.Application.UseCases.Payments.Queries.GetInvoiceDetailsByOrderIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerenceBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStripeService _stripeService;
        private readonly ICurrentUserService _currentUserService;


        public PaymentController(IMediator mediator, IStripeService stripeService, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _stripeService = stripeService;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand command)
        {
            var paymentId = await _mediator.Send(command);
            return Ok(CreatedAtAction(nameof(CreatePayment), new { id = paymentId }, paymentId));
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetInvoiceDetailsByOrderId(Guid orderId)
        {
            var payments = await _mediator.Send(new GetInvoiceDetailsByOrderIdQuery(orderId));
            if (payments == null || payments.Count == 0) return NotFound();
            return Ok(payments);
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CreateStripeCheckout([FromBody] List<CheckoutItemDto> items)
        {
            decimal tax = 0;
            decimal shipping = 0;
            decimal discount = 0;
            var userId = _currentUserService.UserId;

            var orderCommand = new CreateOrderCommand
            {
                CustomerId = CustomerId.Create(Guid.Parse(userId!)), 
                Items = items.Select(x => new OrderItemDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    Quantity = x.Quantity

                }).ToList(),
                PaymentMethod = "Stripe",
                TaxAmount = tax,
                ShippingCost = shipping,
                DiscountAmount = discount,
            };

            var orderId = await _mediator.Send(orderCommand);
            var session = await _stripeService.CreateCheckoutSessionAsync(items, orderId);

            return Ok(new
            {
                sessionUrl = session.Url,
                sessionId = session.SessionId
            });
        }

    }
}
