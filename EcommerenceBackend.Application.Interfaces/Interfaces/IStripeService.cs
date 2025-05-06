using EcommerenceBackend.Application.Dto.ShoppingCart.Response;
using EcommerenceBackend.Application.Dto.Stripe.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Interfaces.Interfaces
{
    public interface IStripeService
    {

        Task<CreateStripeCheckoutResponse> CreateCheckoutSessionAsync(List<CheckoutItemDto> items, Guid orderId);

    }

}
