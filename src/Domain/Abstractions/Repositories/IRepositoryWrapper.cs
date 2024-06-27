namespace Domain.Abstractions.Repositories;

public interface IRepositoryWrapper
{
    IBillingRepository Billing { get; }
    ICustomerRepository Customer { get; }
    IProductRepository Product { get; }

    Task OpenTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);

    Task SaveAsync(CancellationToken cancellationToken = default);
}
