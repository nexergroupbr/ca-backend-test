namespace Domain.Entities
{
    public class Billing
    {
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public int Id { get; set; }
        public int BillingId { get; set; }
        public string InvoiceNumber { get; set; }
        public string Currency { get; set; }
        public decimal Total_Amount { get; set; }
        public DateOnly Date { get; set; }
        public ICollection<BillingLine> BillingLines { get; set; }
    }
}
