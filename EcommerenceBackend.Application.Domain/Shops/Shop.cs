using EcommerenceBackend.Application.Domain.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Shops
{
    [Table("shops")]

    public class Shop
    {
        public Guid ShopId { get; set; }
        public string ShopName { get; set; } = string.Empty;
        public int OwnerId { get; set; }
        public string Location { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsVerified { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
