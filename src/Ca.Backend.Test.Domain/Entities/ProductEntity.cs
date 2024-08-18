namespace Ca.Backend.Test.Domain.Entities;
public class ProductEntity : BaseEntity
{
    public string? Description { get; set; }
    public List<BillingLineEntity>? Lines { get; set; }
}

