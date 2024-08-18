using FluentValidation;
using Ca.Backend.Test.Application.Models.Request;

namespace Ca.Backend.Test.Application.Validators;
public class BillingRequestValidator : AbstractValidator<BillingRequest>
{
    public BillingRequestValidator()
    {
        RuleFor(b => b.CustomerId)
            .NotEmpty().WithMessage("Customer id is required.");
            
        RuleFor(x => x.InvoiceNumber)
            .NotEmpty().WithMessage("Invoice number is required.")
            .MaximumLength(50).WithMessage("Invoice number cannot exceed 50 characters.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("Due date is required.")
            .GreaterThanOrEqualTo(x => x.Date).WithMessage("Due date must be equal to or after the billing date.");

        RuleFor(x => x.TotalAmount)
            .NotEmpty().WithMessage("Total amount is required.")
            .GreaterThan(0).WithMessage("Total amount must be greater than zero.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .Length(3).WithMessage("Currency must be exactly 3 characters.");
    }
}

