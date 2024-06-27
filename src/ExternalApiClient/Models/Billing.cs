using System.Text.Json.Serialization;

#nullable disable

namespace ExternalApiClient.Models;

public record Billing
{
    [JsonPropertyName("invoice_number")]
    public string InvoiceNumber { get; set; }

    public DateOnly Date { get; set; }

    [JsonPropertyName("due_date")]
    public DateOnly DueDate { get; set; }

    [JsonPropertyName("total_amount")]
    public decimal TotalAmount { get; set; }

    public string Currency { get; set; }


    public Customer Customer { get; set; }

    public List<BillingLine> Lines { get; set; }
}
