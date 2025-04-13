using EcommerenceBackend.Application.Domain.ShoppingCart;
using EcommerenceBackend.Application.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using System.Reflection.Emit;

namespace EcommerenceBackend.Infrastructure.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ProductId)
                .HasConversion(
                    v => v.Value,
                    v => ProductId.Create(v));

            builder.HasOne(e => e.ShoppingCart)
                .WithMany(x => x.Items)
                .HasForeignKey(e => e.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.ShoppingCart)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }

}
