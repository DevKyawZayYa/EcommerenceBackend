using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.ShoppingCart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EcommerenceBackend.Infrastructure.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {

            builder.Property(e => e.CustomerId)
                .HasConversion(
                    v => v.Value,
                    v => CustomerId.Create(v));

            builder.HasKey(x => x.ShoppingCartId);

            builder.HasMany(x => x.Items)
                .WithOne(x => x.ShoppingCart)
                .HasForeignKey(x => x.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.Items)
                .UsePropertyAccessMode(PropertyAccessMode.Field); // 🧠 this fixes the problem




        }
    }
}
