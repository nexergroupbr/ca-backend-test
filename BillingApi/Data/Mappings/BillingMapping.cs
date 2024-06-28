using BillingApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillingApi.Data.Mappings
{
    public class BillingMapping : IEntityTypeConfiguration<Billing>
    {
        public void Configure(EntityTypeBuilder<Billing> builder)
        {
            builder.ToTable("Billing");
            builder.HasKey(e => e.Id).HasName("PK_IdBilling");
            builder.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(e => e.InvoiceNumber).HasColumnName("InvoiceNumber").IsRequired(true);
            builder.Property(e => e.Date).HasColumnName("Date").IsRequired(true);
            builder.Property(e => e.DueDate).HasColumnName("DueDate").IsRequired(true);
            builder.Property(e => e.TotalAmount).HasColumnName("TotalAmount").IsRequired(true);
            builder.Property(e => e.Currency).HasColumnName("Currency").IsRequired(true);

            builder.Property(e => e.CustomerId).HasColumnName("CustomerId").IsRequired(true);
            builder.HasOne(ed => ed.Customer)
                   .WithMany(e => e.Billing)
                   .HasForeignKey(ed => ed.CustomerId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
