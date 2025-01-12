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

            // Change the constructor to be public
            public ProductId(Guid value)
            {
                Value = value;
            }

            public static ProductId Create(Guid value) => new ProductId(value);

            public override bool Equals(object obj) => obj is ProductId other && Equals(other);

            public bool Equals(ProductId other) => Value.Equals(other.Value);

            public override int GetHashCode() => Value.GetHashCode();

            public override string ToString() => Value.ToString();
        }
    }
}
