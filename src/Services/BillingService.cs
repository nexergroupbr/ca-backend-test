using IRepository = Domain.Abstractions.Repositories.IBillingRepository;

namespace Services;

public class BillingService(IRepositoryWrapper RepositoryWrapper)
    : BaseService,
    IBillingService
{
    protected IRepository Repository => RepositoryWrapper.Billing;

    public async Task<ServiceResult<TModel>> GetByIdAsync<TModel>(Guid id,
                                                                  CancellationToken cancellationToken = default)
    {
        TModel? value = await Repository.GetByIdAsync<TModel>(id, cancellationToken);

        if (value is null)
        {
            return NotFound();
        }

        return Success(value);
    }

    public async Task<ServiceResult<List<TModel>>> GetAllAsync<TModel>(Pagination pagination,
                                                                       CancellationToken cancellationToken = default)
    {
        pagination ??= new();

        List<TModel> values = await Repository.GetAllAsync<TModel>(pagination, cancellationToken);

        return Success(values);
    }

    public async Task<ServiceResult> DeleteAsync(Guid id,
                                                 CancellationToken cancellationToken = default)
    {
        if (!(await Repository.ExistsAsync(id, cancellationToken)))
        {
            return NotFound();
        }

        await Repository.DeleteAsync(id, cancellationToken);

        return Success();
    }
}
