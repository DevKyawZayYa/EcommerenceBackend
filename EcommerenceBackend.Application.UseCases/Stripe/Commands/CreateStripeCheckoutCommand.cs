using EcommerenceBackend.Application.Dto.ShoppingCart.Response;
using EcommerenceBackend.Application.Dto.Stripe.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Stripe.Commands
{
    public class CreateStripeCheckoutCommand : IRequest<CreateStripeCheckoutResponse>
    {
        public List<CheckoutItemDto> Items { get; set; } = new();
    }
}
