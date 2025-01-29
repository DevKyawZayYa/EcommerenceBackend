namespace EcommerenceBackend.Application.Domain.Products
{
    public class Money
    {
        public decimal Amount { get; private set; }

        // Parameterless constructor for EF Core
        private Money() { }

        // Constructor with parameters for business logic
        public Money(decimal amount)
        {
            Amount = amount;
        }
    }
}
