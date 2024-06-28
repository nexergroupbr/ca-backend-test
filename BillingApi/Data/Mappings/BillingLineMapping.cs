using BillingApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillingApi.Data.Mappings
{
    public class BillingLineMapping : IEntityTypeConfiguration<BillingLine>
    {
        public void Configure(EntityTypeBuilder<BillingLine> builder)
        {
            builder.ToTable("BillingLine");
            builder.HasKey(e => e.Id).HasName("PK_IdBillingLine");
            builder.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(e => e.ProductId).HasColumnName("ProductId").IsRequired(true);
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired(true);
            builder.Property(e => e.Quantity).HasColumnName("Quantity").IsRequired(true);
            builder.Property(e => e.UnitPrice).HasColumnName("UnitPrice").IsRequired(true);
            builder.Property(e => e.SubTotal).HasColumnName("SubTotal").IsRequired(true);

            builder.Property(e => e.ProductId).HasColumnName("ProductId").IsRequired(true);
            builder.HasOne(ed => ed.Product)
                    .WithMany(e => e.BillingLine)
                    .HasForeignKey(ed => ed.ProductId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.BillingId).HasColumnName("BillingId").IsRequired(true);
            builder.HasOne(ed => ed.Billing)
                    .WithMany(e => e.BillingLines)
                    .HasForeignKey(ed => ed.BillingId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
