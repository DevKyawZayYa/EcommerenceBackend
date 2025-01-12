namespace EcommerenceBackend.Application.Domain.ValueObjects
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

    public class LastName
    {
        public string Value { get; }

        public LastName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Last name cannot be empty.", nameof(value));

            Value = value;
        }
    }
}
