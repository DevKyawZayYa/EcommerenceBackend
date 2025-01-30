namespace EcommerenceBackend.Application.Domain.Products
{
    // EcommerenceBackend.Application.Domain/Products/Money.cs
    public class Money
    {
        public decimal Amount { get; private set; }

        private Money() { } // Required for EF Core

        public Money(decimal amount)
        {
            Amount = amount;
        }

        public static implicit operator decimal(Money money) => money.Amount;
        public static explicit operator Money(decimal amount) => new(amount);
    }
}
