namespace Domain.Abstractions.Services;

public interface IBillingService
{
    Task<ServiceResult<TModel>> GetByIdAsync<TModel>(Guid id, CancellationToken cancellationToken = default);

    Task<ServiceResult<List<TModel>>> GetAllAsync<TModel>(Pagination pagination, CancellationToken cancellationToken = default);

    Task<ServiceResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
