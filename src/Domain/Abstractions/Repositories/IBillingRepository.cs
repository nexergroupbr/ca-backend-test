namespace Domain.Abstractions.Repositories;

public interface IBillingRepository
    : IEntityRepository<Billing>
{
    Task<bool> ExistsAsync(string invoiceNumber, CancellationToken cancellationToken = default);
}
