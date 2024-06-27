namespace Infraestructure.EntitiesConfiguration;

public class BillingLinesConfiguration : IEntityTypeConfiguration<BillingLine>
{
    public void Configure(EntityTypeBuilder<BillingLine> builder)
    {
        builder.HasKey(p => new { p.BillingId, p.ProductId });
    }
}
