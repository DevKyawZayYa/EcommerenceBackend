using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerenceBackend.Infrastructure.Configurations.Entities
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasConversion(
                productId => productId.Value,
                value => new ProductId(value));

            builder.OwnsOne(p => p.Price, p =>
            {
                p.Property(m => m.Amount).HasColumnName("Price");
            });

            builder.OwnsOne(p => p.Sku, p =>
            {
                p.Property(s => s.Value).HasColumnName("Sku");
            });

            builder.HasMany(p => p.ProductImages)
                .WithOne()
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
