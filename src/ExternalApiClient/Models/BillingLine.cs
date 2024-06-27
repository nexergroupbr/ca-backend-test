using System.Text.Json.Serialization;

#nullable disable

namespace ExternalApiClient.Models;

public record BillingLine
{
    public Guid ProductId { get; set; }

    public string Description { get; set; }

    public int Quantity { get; set; }

    [JsonPropertyName("unit_price")]
    public decimal UnitPrice { get; set; }

    public decimal Subtotal { get; set; }
}
