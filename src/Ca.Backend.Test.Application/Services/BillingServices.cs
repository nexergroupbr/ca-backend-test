using System.Text.Json;
using AutoMapper;
using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;
using Ca.Backend.Test.Application.Models.Response.Api;
using Ca.Backend.Test.Application.Services.Interfaces;
using Ca.Backend.Test.Domain.Entities;
using Ca.Backend.Test.Infra.Data.Repository.Interfaces;
using FluentValidation;

namespace Ca.Backend.Test.Application.Services;
public class BillingServices : IBillingServices
{
    private readonly IGenericRepository<BillingEntity> _repository;
    private readonly IGenericRepository<CustomerEntity> _customerRepository;
    private readonly IGenericRepository<ProductEntity> _productRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<BillingRequest> _billingRequestValidator;
    private readonly HttpClient _httpClient;

    public BillingServices(IGenericRepository<BillingEntity> repository,
                            IGenericRepository<CustomerEntity> customerRepository,
                            IMapper mapper, IValidator<BillingRequest> billingRequestValidator,
                            IGenericRepository<ProductEntity> productRepository,
                            HttpClient httpClient)
    {
        _repository = repository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _billingRequestValidator = billingRequestValidator;
        _productRepository = productRepository;
        _httpClient = httpClient;
    }
    
    public async Task ImportBillingFromExternalApiAsync()
    {
        var billingData = await RequestExternalBillings();
        
        var firstBilling = billingData.First();

        // Validate customer
        await VerifyCustomerExistsAsync(firstBilling.Customer.Id);

        // Validate products
        await VerifyProductsExistsAsync(firstBilling.Lines.Select(l => l.ProductId));
        
        // Map and save billing data using AutoMapper
        var billingEntity = _mapper.Map<BillingEntity>(firstBilling);
        await _repository.CreateAsync(billingEntity);
    }

    private async Task<IList<BillingApiResponse>> RequestExternalBillings()
    {
        // Fetch billing data from external API
        var httpRequestMessage = new HttpRequestMessage
        {
            RequestUri = new Uri("https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing"),
            Method = HttpMethod.Get
        };
        
        var response = await _httpClient.SendAsync(httpRequestMessage, new CancellationToken());
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var billingData = JsonSerializer.Deserialize<List<BillingApiResponse>>(responseContent);

        if (billingData is null || !billingData.Any())
            throw new ApplicationException("No billing data found in the API.");

        return billingData;
    }

    public async Task<BillingResponse> CreateAsync(BillingRequest billingRequest)
    {
        var validationResult = await _billingRequestValidator.ValidateAsync(billingRequest);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        await VerifyCustomerExistsAsync(billingRequest.CustomerId);

        await VerifyProductsExistsAsync(billingRequest.Lines.Select(l => l.ProductId));

        var billingEntity = _mapper.Map<BillingEntity>(billingRequest);
        billingEntity = await _repository.CreateAsync(billingEntity);
        return _mapper.Map<BillingResponse>(billingEntity);
    }

    private async Task VerifyProductsExistsAsync(IEnumerable<Guid> productIds)
    {
        foreach(var id in productIds)
        {
            var productEntity = await _productRepository.GetByIdAsync(id);
            if(productEntity is null)
                throw new ApplicationException($"Product with ID {id} not found.");
        }
    }

    private async Task VerifyCustomerExistsAsync(Guid id)
    {
        var customerEntity = await _customerRepository.GetByIdAsync(id);
        if (customerEntity is null)
            throw new ApplicationException($"Customer with ID {id} not found.");
    }

    public async Task<BillingResponse> GetByIdAsync(Guid id)
    {
        var billingEntity = await _repository.GetByIdAsync(id);

        if (billingEntity is null)
            throw new ApplicationException($"Billing with ID {id} not found.");

        return _mapper.Map<BillingResponse>(billingEntity);
    }

    public async Task<IEnumerable<BillingResponse>> GetAllAsync()
    {
        var billingEntities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<BillingResponse>>(billingEntities);
    }

    public async Task<BillingResponse> UpdateAsync(Guid id, BillingRequest billingRequest)
    {
        var validationResult = await _billingRequestValidator.ValidateAsync(billingRequest);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var billingEntity = await _repository.GetByIdAsync(id);
        if (billingEntity is null)
            throw new ApplicationException($"Billing with ID {id} not found.");

        await VerifyCustomerExistsAsync(billingRequest.CustomerId);

        await VerifyProductsExistsAsync(billingRequest.Lines.Select(l => l.ProductId));

        _mapper.Map(billingRequest, billingEntity);
        billingEntity = await _repository.UpdateAsync(billingEntity);
        return _mapper.Map<BillingResponse>(billingEntity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var billingEntity = await _repository.GetByIdAsync(id);

        if (billingEntity is null)
            throw new ApplicationException($"Billing with ID {id} not found.");

        await _repository.DeleteByIdAsync(id);
    }

}

