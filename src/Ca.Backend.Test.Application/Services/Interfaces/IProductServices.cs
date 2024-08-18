using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;

namespace Ca.Backend.Test.Application.Services.Interfaces;
public interface IProductServices
{   
    Task<ProductResponse> CreateAsync(ProductRequest productRequest);
    Task<ProductResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductResponse>> GetAllAsync();
    Task<ProductResponse> UpdateAsync(Guid id, ProductRequest productRequest);
    Task DeleteByIdAsync(Guid id);
}
