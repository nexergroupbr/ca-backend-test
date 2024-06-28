using System.Text.Json.Serialization;

namespace BillingApi.Domain.Models
{
    public class BillingLine
    {
        public int Id { get; set; }

        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unit_price")]
        public int UnitPrice { get; set; }

        [JsonPropertyName("subtotal")]
        public int SubTotal { get; set; }
        public int BillingId { get; set; }
        public Billing Billing { get; set; }
    }
}
