using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Billing.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
    private readonly BillingDbContext _context;

    public CustomerRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Customer customer)
        => await _context.Customers.AddAsync(customer);

    public async Task DeleteAsync(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
            _context.Customers.Remove(customer);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
        => await _context.Customers.ToListAsync();

    public async Task<Customer> GetByEmailAsync(string email)
        => await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);

    public async Task<Customer> GetByIdAsync(Guid id)
        => await _context.Customers.FindAsync(id);

    public Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        return Task.CompletedTask;
    }
}