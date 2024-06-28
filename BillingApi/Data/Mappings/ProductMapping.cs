using BillingApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillingApi.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(e => e.Id).HasName("PK_IdProduct");
            builder.Property(e => e.Id).HasColumnName("Id").IsRequired(true);
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired(true);
        }
    }
}
