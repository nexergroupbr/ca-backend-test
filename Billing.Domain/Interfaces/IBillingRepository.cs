using Billing.Domain.Entities;

namespace Billing.Domain.Interfaces
{
    public interface IBillingRepository
    {
        Task<Billings> GetByIdAsync(Guid id);
        Task<IEnumerable<Billings>> GetAllAsync();
        Task AddAsync(Billings billing);
        Task DeleteAsync(Guid id);
    }
}
