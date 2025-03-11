using EcommerenceBackend.Application.Domain.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Users;

namespace EcommerenceBackend.Infrastructure.Configurations.Entities
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasConversion(
                customerId => customerId.Value,
                value => new CustomerId(value));

            builder.Property(c => c.UserId).HasConversion(
            userId => userId.Value,
            value => new UserId(value));
        }
    }
}
