using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ca.Backend.Test.Application.Models.Response.Api;
public class BillingApiResponse
{
    [JsonPropertyName("invoice_number")]
    public string InvoiceNumber { get; set; }

    [JsonPropertyName("customer")]
    public CustomerApiResponse Customer { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("due_date")]
    public DateTime DueDate { get; set; }

    [JsonPropertyName("total_amount")]
    public decimal TotalAmount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("lines")]
    public List<LineApiResponse> Lines { get; set; }
}

