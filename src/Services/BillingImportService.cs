using Domain.Entities;
using ExternalApiClient;

namespace Services;

public class BillingImportService(IRepositoryWrapper RepositoryWrapper,
                                  BillingClient billingClient,
                                  IMapper mapper)
    : BaseService,
    IBillingImportService
{
    public async Task<ServiceResult> ImportAsync(CancellationToken cancellationToken = default)
    {
        List<KeyValuePair<string, string>> errors = [];
        List<ExternalApiClient.Models.Billing> response;

        try
        {
            response = await billingClient.GetAllAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return new InternalErrorServiceResult(ex.Message);
        }

        foreach (var billing in response)
        {
            if (string.IsNullOrWhiteSpace(billing.InvoiceNumber))
            {
                continue;
            }

            bool customerExists = await RepositoryWrapper.Customer.ExistsAsync(billing.Customer.Id, cancellationToken);
            if (!customerExists)
            {
                errors.Add(new KeyValuePair<string, string>(billing.InvoiceNumber, $"\"{billing.Customer.Name}\" - Customer not found! Please enter it manually."));
            }

            bool productExists = true;
            foreach (var line in billing.Lines)
            {
                if (!(await RepositoryWrapper.Product.ExistsAsync(line.ProductId, cancellationToken)))
                {
                    productExists = false;
                    errors.Add(new KeyValuePair<string, string>(billing.InvoiceNumber, $"\"{line.Description}\" - Product not found! Please enter it manually."));
                }
            }

            if (!customerExists || !productExists || (await RepositoryWrapper.Billing.ExistsAsync(billing.InvoiceNumber, cancellationToken)))
            {
                continue;
            }

            Billing entity = mapper.Map<Billing>(billing);
            RepositoryWrapper.Billing.Create(entity);
        }

        await RepositoryWrapper.SaveAsync(cancellationToken);

        if (errors.Count > 0)
        {
            return ValidationError(errors);
        }

        return Success();
    }
}
