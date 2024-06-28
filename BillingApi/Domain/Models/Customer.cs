using System.Text.Json.Serialization;

namespace BillingApi.Domain.Models
{
    public class Customer
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonIgnore]
        public ICollection<Billing> Billing { get; set; }
    }
}
