using Ca.Backend.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ca.Backend.Test.Infrastructure.Data.Configurations;
public class BillingEntityConfiguration : IEntityTypeConfiguration<BillingEntity>
{
    public void Configure(EntityTypeBuilder<BillingEntity> builder)
    {
        builder.ToTable("Billings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .IsRequired();

        builder.Property(b => b.CustomerId)
            .IsRequired();

        builder.Property(b => b.InvoiceNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.CustomerId)
            .IsRequired();

        builder.Property(b => b.Date)
            .IsRequired();

        builder.Property(b => b.DueDate)
            .IsRequired();

        builder.Property(b => b.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(b => b.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.HasOne(b => b.Customer)
            .WithMany(c => c.Billings)
            .HasForeignKey(b => b.CustomerId);
    }
}

