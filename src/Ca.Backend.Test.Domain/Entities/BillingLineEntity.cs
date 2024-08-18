namespace Ca.Backend.Test.Domain.Entities;
public class BillingLineEntity : BaseEntity
{
    public Guid BillingId { get; set; }
    public BillingEntity? Billing { get; set; }
    public Guid ProductId { get; set; }
    public ProductEntity? Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
}

