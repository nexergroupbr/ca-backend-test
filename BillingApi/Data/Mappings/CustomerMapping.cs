using BillingApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillingApi.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(e => e.Id).HasName("PK_IdCustomer");
            builder.Property(e => e.Id).HasColumnName("Id").IsRequired(true);
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired(true);
            builder.Property(e => e.Email).HasColumnName("Email").IsRequired(true);
            builder.Property(e => e.Address).HasColumnName("Address").IsRequired(true);
        }
    }
}
