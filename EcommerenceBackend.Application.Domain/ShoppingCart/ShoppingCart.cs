using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerenceBackend.Application.Domain.ShoppingCart
{
    public class ShoppingCart
    {
        public Guid ShoppingCartId { get; private set; }
        public CustomerId CustomerId { get; private set; }

        private readonly List<CartItem> _items = new();
        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
        public DateTime UpdatedAt { get; private set; }

        private ShoppingCart() { } // Required for EF Core

        public ShoppingCart(CustomerId customerId)
        {
            ShoppingCartId = Guid.NewGuid();
            CustomerId = customerId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddItem(ProductId productId, decimal price, int quantity)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.UpdateQuantity(existingItem.Quantity + quantity);
            }
            else
            {
                _items.Add(new CartItem(productId, price, quantity));
            }
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveItem(ProductId productId)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
                _items.Remove(item);

            UpdatedAt = DateTime.UtcNow;
        }

        public void ClearCart()
        {
            _items.Clear();
            UpdatedAt = DateTime.UtcNow;
        }

        public decimal CalculateTotalPrice()
        {
            return _items.Sum(i => i.Price * i.Quantity);
        }
    }

    public class CartItem
    {
        public Guid Id  { get; private set; }
        public Guid ShoppingCartId { get; private set; } 
        public ProductId ProductId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        private CartItem() { } // Required for EF Core

        public CartItem(ProductId productId, decimal price, int quantity)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public void UpdateQuantity(int quantity) => Quantity = quantity;
    }
}
