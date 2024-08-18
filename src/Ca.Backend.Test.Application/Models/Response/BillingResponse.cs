namespace Ca.Backend.Test.Application.Models.Response;
public class BillingResponse
{
    public Guid Id { get; set; }
    public string? InvoiceNumber { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Currency { get; set; }
    public List<BillingLineResponse>? Lines { get; set; }
}

