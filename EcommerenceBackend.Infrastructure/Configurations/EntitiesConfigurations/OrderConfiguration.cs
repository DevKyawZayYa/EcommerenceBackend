// EcommerenceBackend.Infrastructure/Configurations/OrderConfiguration.cs
using EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;

namespace EcommerenceBackend.Infrastructure.Configurations.Entities
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.CustomerId).HasConversion(
                customerId => customerId.Value,
                value => new CustomerId(value));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired();

            // Configure other properties if needed
        }
    }
}
