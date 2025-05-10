using EcommerenceBackend.Application.Dto.ShoppingCart.Response;
using EcommerenceBackend.Application.Dto.Stripe.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe.Checkout;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Infrastructure.Contexts;


namespace EcommerenceBackend.Infrastructure.Services
{
    public class StripeService : IStripeService
    {
        public async Task<CreateStripeCheckoutResponse> CreateCheckoutSessionAsync(List<CheckoutItemDto> items, Guid orderId)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = items.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "myr",
                        UnitAmount = (long)(item.Price * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName
                        }
                    },
                    Quantity = item.Quantity
                }).ToList(),
                Mode = "payment",
                SuccessUrl = "https://www.nshoppe.shop/payment-success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://www.nshoppe.shop/payment-cancel",
                Metadata = new Dictionary<string, string>
        {
            { "orderId", orderId.ToString() }
        }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return new CreateStripeCheckoutResponse
            {
                SessionId = session.Id,
                Url = session.Url,
                AmountTotal = session.AmountTotal ?? 0,
                Currency = session.Currency
            };
        }
        public Task<CreateStripeCheckoutResponse> CreateCheckoutSessionAsync(List<ShoppingCartDto> items)
        {
            throw new NotImplementedException();
        }
    }

}
