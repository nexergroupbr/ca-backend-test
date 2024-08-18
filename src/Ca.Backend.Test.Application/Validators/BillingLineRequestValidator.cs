using FluentValidation;
using Ca.Backend.Test.Domain.Entities;

namespace Ca.Backend.Test.Application.Validators;
public class BillingLineEntityValidator : AbstractValidator<BillingLineEntity>
{
    public BillingLineEntityValidator()
    {
        RuleFor(bl => bl.ProductId)
            .NotEmpty().WithMessage("Product id is required.");
            
        RuleFor(bl => bl.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(bl => bl.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.");

        RuleFor(bl => bl.Subtotal)
            .GreaterThanOrEqualTo(0).WithMessage("Subtotal must be greater than or equal to zero.");
    }
}

