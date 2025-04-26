using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Infrastructure.Configurations.Entities
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasConversion(
                userId => userId.Value,
                value => new UserId(value));     
        }
    }
}
