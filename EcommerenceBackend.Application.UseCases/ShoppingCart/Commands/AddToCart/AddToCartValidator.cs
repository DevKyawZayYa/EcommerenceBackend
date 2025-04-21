using FluentValidation;
using EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.AddToCart;

public class AddToCartValidator : AbstractValidator<AddCartItemCommand>
{
    public AddToCartValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
