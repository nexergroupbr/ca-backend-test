using System.ComponentModel.DataAnnotations;

namespace Billing.Domain.Entities
{

    public class Product
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
