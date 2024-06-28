using BillingApi.Domain.Models;
using BillingApi.ViewModels;

namespace BillingApi.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer?> GetCustomer(Guid idCustomer);
        Task<Guid?> PostCustomer(CustomerViewModel customer);
        Task PutCustomer(CustomerViewModel customer);
        Task DeleteCustomer(Guid id);
    }
}
