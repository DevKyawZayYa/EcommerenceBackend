using MediatR;
using EcommerenceBackend.Application.UseCases.Orders.Commands.UpdateOrderPaymentStatus;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Microsoft.EntityFrameworkCore;
using EcommerenceBackend.Infrastructure.Contexts;
using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;

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
            _logger.LogError("Stripe Webhook signature error: " + ex.ToString());
            return BadRequest();
        }

        switch (stripeEvent.Type)
        {
            case "checkout.session.completed":
                var session = stripeEvent.Data.Object as Session;
                _logger.LogInformation($"Checkout session completed: {session?.Id}");

                if (session?.Metadata != null && session.Metadata.ContainsKey("orderId"))
                {
                    if (System.Guid.TryParse(session.Metadata["orderId"], out var orderId))
                    {
                        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
                        if (order != null)
                        {
                            order.SetStripeSessionId(session.Id);
                            await _dbContext.SaveChangesAsync();

                            await _mediator.Send(new UpdateOrderPaymentStatusCommand
                            {
                                OrderId = orderId,
                                NewStatus = "Paid"
                            });
                        }
                        else
                        {
                            _logger.LogWarning("Order not found for metadata orderId");
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Invalid orderId format in metadata");
                    }
                }
                break;

            case "payment_intent.payment_failed":
                var failedIntent = stripeEvent.Data.Object as PaymentIntent;
                _logger.LogWarning($"Payment failed: {failedIntent?.Id}");

                if (failedIntent?.Metadata != null && failedIntent.Metadata.ContainsKey("orderId"))
                {
                    if (System.Guid.TryParse(failedIntent.Metadata["orderId"], out var orderId))
                    {
                        await _mediator.Send(new UpdateOrderPaymentStatusCommand
                        {
                            OrderId = orderId,
                            NewStatus = "Failed"
                        });
                    }
                }
                break;

            default:
                _logger.LogInformation($"Unhandled event type: {stripeEvent.Type}");
                break;
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
