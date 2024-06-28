using BillingApi.Domain.Models;
using System.Text.Json.Serialization;

namespace BillingApi.Service.Dto
{
    public class RootObjectBilling
    {
        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("invoiceAmount")]
        public string InvoiceAmount { get; set; }

        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("lines")]
        public List<BillingLine> BillingLines { get; set; }

        [JsonPropertyName("invoiceDate")]
        public int InvoiceDate { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("query")]
        public string Query { get; set; }

        //[JsonPropertyName("invoices")]
        //public List<GetBillingDto> Invoices { get; set; }

        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("total_amount")]
        public int TotalAmount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        
        //[JsonPropertyName("lines")]
        //public ICollection<BillingLine> BillingLines { get; set; }
    }
}
