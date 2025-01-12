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
        public class OrderId : IEquatable<OrderId>
        {
            public Guid Value { get; }

            // Change the constructor to be public
            public OrderId(Guid value)
            {
                Value = value;
            }

            public static OrderId Create(Guid value) => new OrderId(value);

            public override bool Equals(object obj) => obj is OrderId other && Equals(other);

            public bool Equals(OrderId other) => Value.Equals(other.Value);

            public override int GetHashCode() => Value.GetHashCode();

            public override string ToString() => Value.ToString();
        }

    }

}
