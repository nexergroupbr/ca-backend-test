using BillingApi.Domain.Models;
using System.Text.Json.Serialization;

namespace BillingApi.ViewModels
{
    public class BillingLineViewModel
    {
        public int? Id { get; set; }

        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }


        [JsonPropertyName("unit_price")]
        public int UnitPrice { get; set; }

        [JsonPropertyName("subtotal")]
        public int SubTotal { get; set; }
        public int BillingId { get; set; }
    }
}
