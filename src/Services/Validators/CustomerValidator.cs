namespace Services.Validators;

public class CustomerValidator : AbstractValidator<CustomerDTO>
{
    public CustomerValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(m => m.Email)
            .NotEmpty()
            .MaximumLength(150)
            .EmailAddress();

        RuleFor(m => m.Address)
            .NotEmpty()
            .MaximumLength(250);
    }
}
