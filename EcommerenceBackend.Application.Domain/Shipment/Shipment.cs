using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Shipment
{
    [Table("shipments")]

    public class Shipment
    {
        public Guid ShipmentID { get; set; } = Guid.NewGuid();
        public Guid OrderID { get; set; } // Foreign Key
        public decimal ShippingCost { get; set; }
        public string Carrier { get; set; } = string.Empty;
        public string TrackingNumber { get; set; } = string.Empty;
        public DateTime ShipmentDate { get; set; } = DateTime.UtcNow;
        public ShipmentStatus DeliveryStatus { get; set; } = ShipmentStatus.Pending;
    }

    public enum ShipmentStatus
    {
        Pending,
        Shipped,
        Delivered,
        Canceled
    }
}
