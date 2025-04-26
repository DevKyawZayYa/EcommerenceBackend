using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Users
{
    public class LastName
    {
        public string Value { get; }

        public LastName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Last name cannot be empty.", nameof(value));

            Value = value;
        }

        public static implicit operator LastName(string v)
        {
            throw new NotImplementedException();
        }
    }
}
