using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Products
{
    public class Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        // Parameterless constructor for EF Core
        private Money() { }

        // Constructor with parameters for business logic
        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
}
