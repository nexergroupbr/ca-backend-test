using System.Text.Json.Serialization;

namespace BillingApi.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<BillingLine> BillingLine { get; set; }
    }
}
