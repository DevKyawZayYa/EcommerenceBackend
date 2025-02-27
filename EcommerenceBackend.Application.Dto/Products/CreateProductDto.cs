﻿using EcommerenceBackend.Application.Domain.Products;

namespace EcommerenceBackend.Application.Dto.Products
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
