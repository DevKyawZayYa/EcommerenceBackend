using EcommerenceBackend.Application.Domain.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerenceBackend.Application.Domain.Customers;

namespace EcommerenceBackend.Infrastructure.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasConversion(
                customerId => customerId.Value,
                value => new CustomerId(value));

            builder.Property(c=> c.Name).HasMaxLength(100);
            builder.Property(c => c.Email).HasMaxLength(100);   
            builder.HasIndex(c=> c.Email).IsUnique();
        }
    }
}
