// Domain/Product.cs stays the same
using EcommerenceBackend.Application.Domain.Images.ProductsImages;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Products
{
    [Table("products")]

    public class Product
    {
        public ProductId? Id { get; private set; }
        public string? Name { get; private set; } = string.Empty;
        public string? Description { get; private set; } = string.Empty;
        public string? Color { get; private set; } = string.Empty;
        public Money? Price { get; private set; }
        public Sku? Sku { get; private set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Guid? ShopId { get; private set; }
        public Guid? CategoryId { get; private set; }

        public List<ProductImages>? ProductImages { get; set; } = [];
    }
}
