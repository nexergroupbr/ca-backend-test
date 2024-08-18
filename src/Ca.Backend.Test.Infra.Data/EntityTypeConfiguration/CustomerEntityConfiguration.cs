using Ca.Backend.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ca.Backend.Test.Infrastructure.Data.Configurations;
public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
{
    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(c => c.Address)
            .IsRequired()
            .HasMaxLength(100);
    }
}
