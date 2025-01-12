using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Users
{
    public class UserId : IEquatable<UserId>
    {
        public Guid Value { get; }

        // Change the constructor to be public
        public UserId(Guid value)
        {
            Value = value;
        }

        public static UserId Create(Guid value) => new UserId(value);

        public override bool Equals(object obj) => obj is UserId other && Equals(other);

        public bool Equals(UserId other) => Value.Equals(other.Value);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
