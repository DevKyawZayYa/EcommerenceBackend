using EcommerenceBackend.Application.Domain.Shipment;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Infrastructure.Configurations.EntitiesConfigurations
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.Property(s => s.DeliveryStatus)
                .HasConversion<string>() // Store ENUM as a string
                .HasMaxLength(20); // Ensure enough space for ENUM values
        }
    }
}
