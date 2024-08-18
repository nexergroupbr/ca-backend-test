using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;

namespace Ca.Backend.Test.Application.Services.Interfaces;

public interface ICustomerServices
{
    Task<CustomerResponse> CreateAsync(CustomerRequest customerRequest);
    Task<CustomerResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<CustomerResponse>> GetAllAsync();
    Task<CustomerResponse> UpdateAsync(Guid id, CustomerRequest customerRequest);
    Task DeleteByIdAsync(Guid id);
}
