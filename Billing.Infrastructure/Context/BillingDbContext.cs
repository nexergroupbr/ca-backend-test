using Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infrastructure.Context
{
    public class BillingDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Billings> Billings { get; set; }
        public DbSet<BillingLine> BillingLines { get; set; }

        public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Billings>()
                .HasMany(b => b.Lines)
                .WithOne()
                .HasForeignKey(bl => bl.BillingId);
        }
    }
}
