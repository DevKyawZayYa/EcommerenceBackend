using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Customers
{
    public class CustomerId : IEquatable<CustomerId>
    {
        public Guid Value { get; }

        // Change the constructor to be public
        public CustomerId(Guid value)
        {
            Value = value;
        }

        public static CustomerId Create(Guid value) => new CustomerId(value);

        public override bool Equals(object obj) => obj is CustomerId other && Equals(other);

        public bool Equals(CustomerId other) => Value.Equals(other.Value);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
