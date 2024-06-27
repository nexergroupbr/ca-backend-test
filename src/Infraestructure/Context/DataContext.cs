namespace Infraestructure.Context;

public class DataContext(DbContextOptions<DataContext> options)
    : DbContext(options)
{
    public DbSet<Billing> Billings { get; set; }
    public DbSet<BillingLine> BillingLines { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

#if DEBUG

        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = new Guid("12081264-5645-407a-ae37-78d5da96fe59"), Name = "Cliente Exemplo 1", Email = "cliente1@example.com", Address = "Rua Exemplo 1, 123", CreatedAt = new DateTime(2024, 06, 26, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2024, 06, 26, 0, 0, 0, DateTimeKind.Utc) }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = new Guid("48c6dc20-a943-4f8f-83ca-1e1cf094a683"), Name = "Produto 1", CreatedAt = new DateTime(2024, 06, 26, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2024, 06, 26, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = new Guid("48c6dc20-a943-4f8f-83ca-1e1cf094a612"), Name = "Produto 2", CreatedAt = new DateTime(2024, 06, 26, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2024, 06, 26, 0, 0, 0, DateTimeKind.Utc) }
        );

#endif
    }
}
