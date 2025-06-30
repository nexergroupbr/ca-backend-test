namespace Billing.Domain.Entities
{
    public class BillingLine
    {
        public Guid Id { get; set; }
        public Guid BillingId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
