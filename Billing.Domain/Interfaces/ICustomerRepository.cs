using Billing.Domain.Entities;

namespace Billing.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(Guid id);
        Task<Customer> GetByEmailAsync(string email);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}
