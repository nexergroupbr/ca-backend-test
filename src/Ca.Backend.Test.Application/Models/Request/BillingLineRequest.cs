namespace Ca.Backend.Test.Application.Models.Request;
public class BillingLineRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
}

