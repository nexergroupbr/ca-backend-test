using ExternalApiClient.Exceptions;
using ExternalApiClient.Models;
using ExternalApiClient.Settings;
using Microsoft.Extensions.Options;
using RestSharp;

namespace ExternalApiClient;

public class BillingClient(IOptions<ApiClientSettings> options)
{
    private readonly RestClient client = new(options.Value.Url);

    public async Task<List<Billing>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var request = new RestRequest("api/v1/billing");
        var response = await client.ExecuteGetAsync<List<Billing>>(request, cancellationToken);

        try
        {
            response.ThrowIfError();
        }
        catch (HttpRequestException ex)
        {
            throw new ApiNotFoundException("Error accessing billing API.", ex);
        }
        catch (Exception ex)
        {
            throw new ApiMalFuncException("Error getting data from billing API.", ex);
        }

        return response.Data ?? [];
    }
}
