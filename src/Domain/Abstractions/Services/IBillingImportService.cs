namespace Domain.Abstractions.Services;

public interface IBillingImportService
{
    Task<ServiceResult> ImportAsync(CancellationToken cancellationToken = default);
}
