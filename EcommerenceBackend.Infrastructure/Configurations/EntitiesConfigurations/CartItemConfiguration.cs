using EcommerenceBackend.Application.Domain.ShoppingCart;
using EcommerenceBackend.Application.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;

namespace EcommerenceBackend.Infrastructure.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(e => new { e.ProductId, e.Price });
            builder.Property(e => e.ProductId)
                .HasConversion(
                    v => v.Value,
                    v => ProductId.Create(v));

            builder.Property<Guid>("ShoppingCartId");
        }
    }
}
