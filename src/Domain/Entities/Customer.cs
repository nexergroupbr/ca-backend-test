#nullable disable

namespace Domain.Entities;

public record Customer
    : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }


    public ICollection<Billing> Billings { get; set; }
}
