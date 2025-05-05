using EcommerenceBackend.Application.Dto.ShoppingCart.Response;
using EcommerenceBackend.Application.Dto.Stripe.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe.Checkout;
using EcommerenceBackend.Application.Interfaces.Interfaces;


namespace EcommerenceBackend.Infrastructure.Services
{
    public class StripeService : IStripeService
    {
        public async Task<CreateStripeCheckoutResponse> CreateCheckoutSessionAsync(List<CheckoutItemDto> items)
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
                SuccessUrl = "https://nshoppe.shop/payment-success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://nshoppe.shop/payment-cancel"

            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return new CreateStripeCheckoutResponse
            {
                SessionId = session.Id,
                Url = session.Url
            };
        }

        public Task<CreateStripeCheckoutResponse> CreateCheckoutSessionAsync(List<ShoppingCartDto> items)
        {
            throw new NotImplementedException();
        }
    }

}
