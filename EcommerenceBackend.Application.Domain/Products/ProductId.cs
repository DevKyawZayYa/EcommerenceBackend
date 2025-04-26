using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Products
{
    using System;

    namespace EcommerenceBackend.Application.Domain.Products
    {
        public class ProductId : IEquatable<ProductId>
        {
            public Guid Value { get; }

            public ProductId(Guid value)
            {
                Value = value;
            }

            public static ProductId Create(Guid value) => new ProductId(value);

            public override bool Equals(object? obj)
            {
                return obj is ProductId other && Value.Equals(other.Value);
            }

            public bool Equals(ProductId? other)
            {
                return other is not null && Value.Equals(other.Value);
            }

            public override int GetHashCode() => Value.GetHashCode();

            public override string ToString() => Value.ToString();

            public static bool operator ==(ProductId? left, ProductId? right)
            {
                if (left is null && right is null) return true;
                if (left is null || right is null) return false;
                return left.Value == right.Value;
            }

            public static bool operator !=(ProductId? left, ProductId? right)
            {
                return !(left == right);
            }
        }
    }
}
