using EcommerenceBackend.Application.Dto.ShoppingCart.Response;
using EcommerenceBackend.Application.Dto.Stripe.Response;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Application.UseCases.Orders.Commands.CreateOrder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Stripe.Commands
{
    public class CreateStripeCheckoutHandler : IRequestHandler<CreateStripeCheckoutCommand, CreateStripeCheckoutResponse>
    {
        private readonly IStripeService _stripeService;

        public CreateStripeCheckoutHandler(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        public async Task<CreateStripeCheckoutResponse> Handle(CreateStripeCheckoutCommand request, CancellationToken cancellationToken)
        {
            return await _stripeService.CreateCheckoutSessionAsync(request.Items, request.OrderId);
        }
    }

}
