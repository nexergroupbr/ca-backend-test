using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Billing.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly BillingDbContext _context;

    public ProductRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product)
        => await _context.Products.AddAsync(product);

    public async Task DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
            _context.Products.Remove(product);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _context.Products.ToListAsync();

    public async Task<Product> GetByIdAsync(Guid id)
        => await _context.Products.FindAsync(id);

    public async Task<Product> GetByNameAsync(string name)
        => await _context.Products.FirstOrDefaultAsync(p => p.Name == name);

    public Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        return Task.CompletedTask;
    }
}