using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Users
{
    public class FirstName
    {
        public string Value { get; }

        public FirstName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("First name cannot be empty.", nameof(value));

            Value = value;
        }
    }
}
