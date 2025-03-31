using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Dto.Stripe.Request
{
    public class StripeCheckoutRequest
    {
        public List<CheckoutItem> Items { get; set; }
    }

    public class CheckoutItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
