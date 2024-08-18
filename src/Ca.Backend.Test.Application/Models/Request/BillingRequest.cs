using Ca.Backend.Test.Domain.Entities;

namespace Ca.Backend.Test.Application.Models.Request;
public class BillingRequest
{
    public Guid CustomerId { get; set; }
    public string? InvoiceNumber { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Currency { get; set; }
    public List<BillingLineRequest>? Lines { get; set; }
}

