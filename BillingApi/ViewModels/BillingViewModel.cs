using BillingApi.Domain.Models;

namespace BillingApi.ViewModels
{
    public class BillingViewModel
    {
        public int? Id { get; set; }
        public string InvoiceNumber { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int TotalAmount { get; set; }
        public string Currency { get; set; }
        public List<BillingLine> BillingLines { get; set; }
    }
}
