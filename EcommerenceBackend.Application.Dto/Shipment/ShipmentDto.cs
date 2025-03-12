using EcommerenceBackend.Application.Domain.Shipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Dto.Shipment
{
    public class ShipmentDto
    {
        public Guid ShipmentID { get; set; }
        public Guid OrderID { get; set; }
        public decimal ShippingCost { get; set; }
        public string Carrier { get; set; } = string.Empty;
        public string TrackingNumber { get; set; } = string.Empty;
        public DateTime ShipmentDate { get; set; }
        public ShipmentStatus DeliveryStatus { get; set; }
    }
}
