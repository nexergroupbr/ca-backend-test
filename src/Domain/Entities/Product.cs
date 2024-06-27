#nullable disable

namespace Domain.Entities;

public record Product
    : BaseEntity
{
    public string Name { get; set; }


    public ICollection<BillingLine> BillingLines { get; set; }
}
