using BillingApi.Infra.Exceptions;
using BillingApi.Service.Interfaces;

namespace BillingApi.Service.Services
{
    public class ApiService(IHttpClientFactory httpClientFactory) : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public string GetData(string url)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("GetBillings");

                var response = httpClient.GetAsync(new Uri(url)).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    throw new ServiceUnavailableException("O serviço está indisponível.");
                }

                throw new ApiException("Erro ao chamar a API.");
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException($"Erro na requisição HTTP: {ex.Message}");
            }
            catch (TaskCanceledException ex)
            {
                throw new ApiException($"A requisição foi cancelada: {ex.Message}");
            }
        }
    }
}
