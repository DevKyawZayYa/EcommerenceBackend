using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Products
{
    //Stock Keeping Unit

    public class Sku
    {
        public string Value { get; private set; }

        public Sku(string value)
        {
            Value = value;
        }
    }

    //public record Sku
    //{
    //    private const int DefaultLength = 15;
    //    private Sku(string Value) => Value = Value;
    //    public string Value { get; init; }

    //    public static Sku? Create(string value)
    //    {
    //        if (string.IsNullOrWhiteSpace(value))
    //        {
    //            return null;
    //        }
    //        if (value.Length != DefaultLength)
    //        {
    //            return null;
    //        }
    //        return new Sku(value);
    //    }
    //}
}
