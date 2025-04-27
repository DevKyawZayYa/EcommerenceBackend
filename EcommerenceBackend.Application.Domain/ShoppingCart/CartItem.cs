using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.ShoppingCart
{
    [Table("cartitems")]

    public class CartItem
    {
        public Guid Id { get; private set; }
        public Guid ShoppingCartId { get; private set; }

        public ShoppingCart ShoppingCart { get; set; }

        public ProductId ProductId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        private CartItem() { }

        public CartItem(ProductId productId, decimal price, int quantity)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public void UpdateQuantity(int quantity) => Quantity = quantity;

         public Product Products { get; set; } // optional nav

    }

}
