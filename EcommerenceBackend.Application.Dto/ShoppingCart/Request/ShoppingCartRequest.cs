using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using System;
using System.Collections.Generic;

namespace EcommerenceBackend.Application.Dto.ShoppingCart.Request
{
    public class ShoppingCartRequest
    {
        public CustomerId? CustomerId { get; set; }
        public List<CartItemRequest> Items { get; set; } = new();
    }

    public class CartItemRequest
    {
        public ProductId? ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
