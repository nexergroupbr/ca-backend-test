using System.Text.Json.Serialization;

namespace BillingApi.Domain.Models
{
    public class Billing
    {
        public int Id { get; set; }

        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("total_amount")]
        public int TotalAmount { get; set; }
        public string Currency { get; set; }

        [JsonPropertyName("lines")]
        public ICollection<BillingLine> BillingLines { get; set; }
    }
}