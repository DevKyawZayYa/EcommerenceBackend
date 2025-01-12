using EcommerenceBackend.Application.Domain.Entities;
using EcommerenceBackend.Application.Domain.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;

namespace EcommerenceBackend.Infrastructure.Configurations
{
    internal class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
    {
        public void Configure(EntityTypeBuilder<LineItem> builder)
        {
            builder.HasKey(li => li.Id);

            builder.Property(li => li.Id).HasConversion(
                lineItemId => lineItemId.Value,
                value => new LineItemId(value));

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(o => o.ProductId);

            builder.OwnsOne(li => li.Price);
        }
    }
}
