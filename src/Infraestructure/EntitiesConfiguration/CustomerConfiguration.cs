namespace Infraestructure.EntitiesConfiguration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Address)
            .HasMaxLength(250)
            .IsRequired();
    }
}
