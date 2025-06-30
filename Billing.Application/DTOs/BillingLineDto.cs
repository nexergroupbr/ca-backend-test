namespace Billing.Application.DTOs
{
    public class BillingLineDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
