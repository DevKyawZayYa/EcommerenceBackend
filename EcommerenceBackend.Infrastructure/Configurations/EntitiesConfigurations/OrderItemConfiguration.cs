using EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerenceBackend.Infrastructure.Configurations.Entities
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Id).HasConversion(
                orderItemId => orderItemId.Value,
                value => new OrderItemId(value));

            builder.Property(oi => oi.ProductId).HasConversion(
                productId => productId.Value,
                value => new ProductId(value));

            builder.OwnsOne(oi => oi.Price, price =>
            {
                price.Property(p => p.Amount).HasColumnName("Price");
            });

            builder.OwnsOne(oi => oi.Quantity, quantity =>
            {
                quantity.Property(q => q.Amount).HasColumnName("Quantity");
            });
        }
    }
}
