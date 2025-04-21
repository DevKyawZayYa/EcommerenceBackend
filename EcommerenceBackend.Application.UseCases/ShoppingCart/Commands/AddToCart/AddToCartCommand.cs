using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Dto.ShoppingCart.Request;
using MediatR;
using System;

namespace EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.AddToCart
{
    public class AddCartItemCommand : IRequest<Guid>
    {
        public CustomerId? CustomerId { get; set; }
        public ProductId? ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

}
