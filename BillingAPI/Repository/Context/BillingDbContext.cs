using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Context
{
    public class BillingDbContext : DbContext
    {
        public BillingDbContext(DbContextOptions<BillingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Billing> Billings { get; set; }

        public DbSet<BillingLine> BillingLines { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Billing>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.HasMany(b => b.BillingLines)
                      .WithOne(bl => bl.Billing)
                      .HasForeignKey(bl => bl.BillingId);
            });

            modelBuilder.Entity<BillingLine>(entity =>
            {
                entity.HasKey(bl => bl.Id);
            });

            modelBuilder.Entity<Billing>()
           .HasOne(b => b.Customer)
           .WithMany(c => c.Billings)
           .HasForeignKey(b => b.CustomerId);

            modelBuilder.Entity<BillingLine>()
           .HasOne(bl => bl.Product)
           .WithMany(p => p.BillingLines)
           .HasForeignKey(bl => bl.ProductId);

          
        }
    }
}
