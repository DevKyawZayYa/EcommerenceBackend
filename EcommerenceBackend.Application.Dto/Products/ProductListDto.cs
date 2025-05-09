using EcommerenceBackend.Application.Domain.Images.ProductsImages;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Dto.Products
{
    public class ProductListDto
    {
        public ProductId Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public Money Price { get; set; }
        public Sku Sku { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? PrimaryImageUrl { get; set; }
    }
}
