namespace Infraestructure.EntitiesConfiguration;

public class BillingConfiguration : IEntityTypeConfiguration<Billing>
{
    public void Configure(EntityTypeBuilder<Billing> builder)
    {
        builder.Property(p => p.InvoiceNumber)
            .HasMaxLength(7)
            .IsRequired();

        builder.Property(p => p.Currency)
            .HasMaxLength(3)
            .IsRequired();

        builder.HasIndex(p => p.InvoiceNumber)
            .IsUnique();
    }
}
