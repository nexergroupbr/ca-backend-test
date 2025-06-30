using System.ComponentModel.DataAnnotations;

namespace Billing.Domain.Entities
{
    public class Customer
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
