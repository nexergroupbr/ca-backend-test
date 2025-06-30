namespace Billing.Application.DTOs
{
    public class BillingsDto
    {
        public Guid CustomerId { get; set; }
        public DateTime BillingDate { get; set; }
        public List<BillingLineDto> Lines { get; set; } = new();
    }
}
