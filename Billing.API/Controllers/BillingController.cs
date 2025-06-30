using Billing.Application.DTOs;
using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Billing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBillingRepository _billingRepository;
        private readonly HttpClient _httpClient;

        public BillingController(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IBillingRepository billingRepository,
            IHttpClientFactory httpClientFactory)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _billingRepository = billingRepository;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("import-first")]
        public async Task<IActionResult> ImportFirstBilling()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var billings = JsonSerializer.Deserialize<List<BillingsDto>>(content, options);

                if (billings == null || !billings.Any())
                    return NotFound("Nenhum faturamento retornado da API externa.");

                var billingDto = billings.First();
                var customer = await _customerRepository.GetByIdAsync(billingDto.CustomerId);
                var missingProducts = new List<Guid>();

                foreach (var line in billingDto.Lines)
                {
                    var product = await _productRepository.GetByIdAsync(line.ProductId);
                    if (product == null)
                        missingProducts.Add(line.ProductId);
                }

                if (customer == null && missingProducts.Any())
                    return BadRequest("Cliente e produtos do billing não existem no banco local.");

                if (customer == null)
                    return BadRequest("Cliente do billing não existe no banco local.");

                if (missingProducts.Any())
                    return BadRequest($"Produtos não encontrados no banco local: {string.Join(", ", missingProducts)}");

                var billing = new Billings
                {
                    Id = Guid.NewGuid(),
                    CustomerId = billingDto.CustomerId,
                    BillingDate = billingDto.BillingDate,
                    Lines = billingDto.Lines.Select(l => new BillingLine
                    {
                        Id = Guid.NewGuid(),
                        BillingId = Guid.NewGuid(),
                        ProductId = l.ProductId,
                        Quantity = l.Quantity,
                        Price = l.Price
                    }).ToList()
                };

                billing.Lines.ForEach(line => line.BillingId = billing.Id);
                await _billingRepository.AddAsync(billing);

                return Ok("Faturamento importado com sucesso.");
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Erro ao se conectar com a API externa de billing.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
