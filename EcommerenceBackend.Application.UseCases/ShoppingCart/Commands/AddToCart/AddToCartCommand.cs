using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Dto.ShoppingCart.Request;
using MediatR;
using System;

namespace EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.AddToCart
{
    public class AddToCartCommand : IRequest<Guid>
    {
        public CustomerId? CustomerId { get; set; }
        public List<CartItemRequest> Items { get; set; } = new();
    }
}
