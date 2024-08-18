using Ca.Backend.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ca.Backend.Test.Infra.Data.Repository;

public class BillingRepository : GenericRepository<BillingEntity>
{
    
    public BillingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<IList<BillingEntity>> GetAllAsync()
    {
        return await _dbSet.Include(b => b.Customer)
                           .Include(b => b.Lines).
                            ToListAsync();
    }
}
