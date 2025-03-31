using EcommerenceBackend.Application.UseCases.Stripe.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerenceBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StripeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StripeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateStripeCheckoutCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }

}
