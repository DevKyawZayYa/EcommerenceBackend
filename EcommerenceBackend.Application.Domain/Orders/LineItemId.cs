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
        public class LineItemId : IEquatable<LineItemId>
        {
            public Guid Value { get; }

            // Make the constructor public to fix the accessibility issue
            public LineItemId(Guid value)
            {
                Value = value;
            }

            public static LineItemId Create(Guid value)
            {
                return new LineItemId(value);
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as LineItemId);
            }

            public bool Equals(LineItemId other)
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
