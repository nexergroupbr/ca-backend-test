using Ca.Backend.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ca.Backend.Test.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<BillingEntity> Billings { get; set; }
    public DbSet<BillingLineEntity> BillingLines { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

