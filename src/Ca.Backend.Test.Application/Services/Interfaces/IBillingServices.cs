using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;

namespace Ca.Backend.Test.Application.Services.Interfaces;
public interface IBillingServices
{
    Task ImportBillingFromExternalApiAsync();
    Task<BillingResponse> CreateAsync(BillingRequest billingRequest);
    Task<BillingResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<BillingResponse>> GetAllAsync();
    Task<BillingResponse> UpdateAsync(Guid id, BillingRequest billingRequest);
    Task DeleteByIdAsync(Guid id);
    
}
