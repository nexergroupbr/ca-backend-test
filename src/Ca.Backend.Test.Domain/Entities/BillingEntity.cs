namespace Ca.Backend.Test.Domain.Entities;
public class BillingEntity : BaseEntity
{
    public string? InvoiceNumber { get; set; }
    public Guid CustomerId { get; set; } 
    public CustomerEntity? Customer { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Currency { get; set; }
    public List<BillingLineEntity>? Lines { get; set; }
}

