namespace Services.Validators;

public class ProductValidator : AbstractValidator<ProductDTO>
{
    public ProductValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(150);
    }
}
