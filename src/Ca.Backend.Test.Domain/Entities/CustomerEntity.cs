namespace Ca.Backend.Test.Domain.Entities;
public class CustomerEntity : BaseEntity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public List<BillingEntity>? Billings { get; set; }
}

