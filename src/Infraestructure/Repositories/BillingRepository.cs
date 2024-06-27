namespace Infraestructure.Repositories;

public class BillingRepository(DataContext repositoryContext, IMapper mapper)
    : EntityRepository<Billing>(repositoryContext, mapper),
    IBillingRepository
{
    public async Task<bool> ExistsAsync(string invoiceNumber, CancellationToken cancellationToken = default)
        => await GetByConditionQuery(q => q.InvoiceNumber == invoiceNumber).AnyAsync(cancellationToken);
}
