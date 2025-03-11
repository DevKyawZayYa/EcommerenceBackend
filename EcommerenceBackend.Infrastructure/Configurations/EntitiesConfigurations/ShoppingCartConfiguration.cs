using EcommerenceBackend.Application.Domain.ShoppingCart;
using EcommerenceBackend.Application.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerenceBackend.Infrastructure.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(e => e.ShoppingCartId);
            builder.Property(e => e.CustomerId)
                .HasConversion(
                    v => v.Value,
                    v => CustomerId.Create(v));

            builder.HasMany(e => e.Items)
               .WithOne()
               .HasForeignKey("ShoppingCartId")
               .OnDelete(DeleteBehavior.Cascade); // Optional: Configure dele
        }
    }
}
