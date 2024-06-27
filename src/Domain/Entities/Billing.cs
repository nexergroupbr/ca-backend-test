#nullable disable

namespace Domain.Entities;

public record Billing
    : BaseEntity
{
    public string InvoiceNumber { get; set; }

    public Guid CustomerId { get; set; }

    public DateOnly Date { get; set; }

    public DateOnly DueDate { get; set; }

    public decimal Total { get; set; }

    public string Currency { get; set; }


    public Customer Customer { get; set; }

    public ICollection<BillingLine> Lines { get; set; }
}
