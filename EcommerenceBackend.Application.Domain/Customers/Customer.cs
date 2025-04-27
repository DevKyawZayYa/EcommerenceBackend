using EcommerenceBackend.Application.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Customers
{
    [Table("customers")]

    public class Customer
    {
        public CustomerId? Id { get; private set; }
        public UserId? UserId { get; private set; }
        public decimal LoyaltyPoint { get; private set; }
        public string? PreferredPaymentMethod { get; private set; }
        public User? User { get; private set; }
    }
}
