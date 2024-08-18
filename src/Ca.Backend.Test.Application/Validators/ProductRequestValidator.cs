using FluentValidation;
using Ca.Backend.Test.Application.Models.Request;

namespace Ca.Backend.Test.Application.Validators;
public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(100).WithMessage("Description cannot exceed 100 characters.");
    }
}

