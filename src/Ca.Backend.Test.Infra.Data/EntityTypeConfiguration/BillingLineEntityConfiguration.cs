using Ca.Backend.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ca.Backend.Test.Infrastructure.Data.Configurations;
public class BillingLineEntityConfiguration : IEntityTypeConfiguration<BillingLineEntity>
{
    public void Configure(EntityTypeBuilder<BillingLineEntity> builder)
    {
        builder.ToTable("BillingLines");

        builder.HasKey(bl => bl.Id);

        builder.Property(bl => bl.Id)
            .IsRequired();

        builder.Property(bl => bl.BillingId)
            .IsRequired();

        builder.Property(bl => bl.ProductId)
            .IsRequired();
        
        builder.Property(bl => bl.Quantity)
            .IsRequired();

        builder.Property(bl => bl.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(bl => bl.Subtotal)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(bl => bl.Billing)
            .WithMany(b => b.Lines)
            .HasForeignKey(bl => bl.BillingId);

        builder.HasOne(bl => bl.Product)
            .WithMany(b => b.Lines)
            .HasForeignKey(bl => bl.ProductId);
    }
}

