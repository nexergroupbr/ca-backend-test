using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Billing.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

public class BillingRepository : IBillingRepository
{
    private readonly BillingDbContext _context;

    public BillingRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Billing.Domain.Entities.Billings billing)
        => await _context.Billings.AddAsync(billing);

    public async Task DeleteAsync(Guid id)
    {
        var billing = await _context.Billings.FindAsync(id);
        if (billing != null)
            _context.Billings.Remove(billing);
    }

    public async Task<IEnumerable<Billing.Domain.Entities.Billings>> GetAllAsync()
        => await _context.Billings
            .Include(b => b.Lines)
            .ToListAsync();

    public async Task<Billing.Domain.Entities.Billings> GetByIdAsync(Guid id)
        => await _context.Billings
            .Include(b => b.Lines)
            .FirstOrDefaultAsync(b => b.Id == id);
}