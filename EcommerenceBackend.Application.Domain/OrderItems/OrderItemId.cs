using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Orders
{
    using System;

    namespace EcommerenceBackend.Application.Domain.Orders
    {
        public class OrderItemId : IEquatable<OrderItemId>
        {
            public Guid Value { get; }

            // Make the constructor public to fix the accessibility issue
            public OrderItemId(Guid value)
            {
                Value = value;
            }

            public static OrderItemId Create(Guid value)
            {
                return new OrderItemId(value);
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as OrderItemId);
            }

            public bool Equals(OrderItemId other)
            {
                return other != null && Value.Equals(other.Value);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Value);
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

    }

}
