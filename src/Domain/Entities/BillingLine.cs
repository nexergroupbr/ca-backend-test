#nullable disable

namespace Domain.Entities;

public record BillingLine
{
    public Guid BillingId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotal { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    public Billing Billing { get; set; }

    public Product Product { get; set; }
}
