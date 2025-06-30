namespace Billing.Domain.Entities
{
    public class Billings
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime BillingDate { get; set; }
        public List<BillingLine> Lines { get; set; } = new();
    }
}
