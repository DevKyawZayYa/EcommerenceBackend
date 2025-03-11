using EcommerenceBackend.Application.UseCases.Payments.Commands.CreatePayment;
using EcommerenceBackend.Application.UseCases.Payments.Queries.GetInvoiceDetailsByOrderIdQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerenceBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
