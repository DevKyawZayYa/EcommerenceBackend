using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Shops
{
    public class ShopOwner
    {
        public Guid ShopOwnerId { get; set; }
        public Guid UserId { get; set; }
        public Guid ShopId { get; set; }
        public string BusinessType { get; set; } = string.Empty;
        public decimal RevenueShare { get; set; }
    }
}
