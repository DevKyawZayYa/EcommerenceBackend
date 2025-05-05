using MediatR;
using EcommerenceBackend.Application.UseCases.Orders.Commands.UpdateOrderPaymentStatus;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Microsoft.EntityFrameworkCore;
using EcommerenceBackend.Infrastructure.Contexts;

[ApiController]
[Route("api/webhooks/stripe")]
public class StripeWebhookController : ControllerBase
{
    private readonly ILogger<StripeWebhookController> _logger;
    private readonly string _webhookSecret;
    private readonly IMediator _mediator;
    private readonly OrderDbContext _dbContext;

    public StripeWebhookController(IConfiguration configuration, ILogger<StripeWebhookController> logger, IMediator mediator, OrderDbContext dbContext)
    {
        _webhookSecret = configuration["Stripe:WebhookSecret"];
        _logger = logger;
        _mediator = mediator;
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Index()
    {
        string json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        Event stripeEvent;
        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _webhookSecret
            );
        }
        catch (Exception ex)
        {
            _logger.LogError($"Stripe Webhook signature error: {ex.Message}");
            return BadRequest();
        }

        if (stripeEvent.Type == "checkout.session.completed")
        {
            var session = stripeEvent.Data.Object as Session;
            _logger.LogInformation($"Stripe Checkout Session completed: {session?.Id}");

            if (session != null)
            {
                await _mediator.Send(new UpdateOrderPaymentStatusCommand
                {
                    StripeSessionId = session.Id,
                    NewStatus = "Paid"
                });
            }
        }
        else
        {
            _logger.LogInformation($"Unhandled event type: {stripeEvent.Type}");
        }
        return Ok();
    }

    [HttpGet("by-session/{sessionId}")]
    public async Task<IActionResult> GetBySession(string sessionId)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.StripeSessionId == sessionId);
        if (order == null) return NotFound();

        return Ok(new
        {
            order.Id,
            order.PaymentStatus,
            order.DeliveryStatus,
            order.OrderDate
        });
    }

}







