using DTO = Domain.Dtos.ProductDTO;
using TEntity = Domain.Entities.Product;

namespace Domain.Abstractions.Services;

public interface IProductService
{
    Task<ServiceResult<TModel>> GetByIdAsync<TModel>(Guid id, CancellationToken cancellationToken = default);

    Task<ServiceResult<List<TModel>>> GetAllAsync<TModel>(Pagination pagination, CancellationToken cancellationToken = default);

    Task<ServiceResult<TEntity>> CreateAsync(DTO dto, CancellationToken cancellationToken = default);

    Task<ServiceResult> UpdateAsync(Guid id, DTO dto, CancellationToken cancellationToken = default);

    Task<ServiceResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
