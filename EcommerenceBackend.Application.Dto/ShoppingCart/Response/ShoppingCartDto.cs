using System;
using System.Collections.Generic;

namespace EcommerenceBackend.Application.Dto.ShoppingCart.Response
{
    public class ShoppingCartDto
    {
        public Guid CartId { get; set; }
        public Guid CustomerId { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
    }

    public class CartItemDto
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; set; }
        public string? Name { get;  set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? PrimaryImageUrl { get; set; }
    }
}
