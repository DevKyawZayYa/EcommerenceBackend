using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Dto.Stripe.Response
{
    public class CreateStripeCheckoutResponse
    {
        public string SessionId { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public long AmountTotal { get; set; }
        public string Currency { get; set; } = string.Empty;
    }


}
