using AutoMapper;
using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;
using Ca.Backend.Test.Application.Services.Interfaces;
using Ca.Backend.Test.Domain.Entities;
using Ca.Backend.Test.Infra.Data.Repository.Interfaces;
using FluentValidation;

namespace Ca.Backend.Test.Application.Services;
public class ProductServices : IProductServices
{
    private readonly IGenericRepository<ProductEntity> _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductRequest> _productRequestValidator;

    public ProductServices(IGenericRepository<ProductEntity> repository, IMapper mapper,
                            IValidator<ProductRequest> productRequestValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _productRequestValidator = productRequestValidator;
    }

    public async Task<ProductResponse> CreateAsync(ProductRequest productRequest)
    {
        var validationResult = await _productRequestValidator.ValidateAsync(productRequest);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var productEntity = _mapper.Map<ProductEntity>(productRequest);
        productEntity = await _repository.CreateAsync(productEntity);
        return _mapper.Map<ProductResponse>(productEntity);
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var productEntity = await _repository.GetByIdAsync(id);

        if (productEntity is null)
            throw new ApplicationException($"Product with ID {id} not found.");

        return _mapper.Map<ProductResponse>(productEntity);
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var productEntities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductResponse>>(productEntities);
    }

    public async Task<ProductResponse> UpdateAsync(Guid id, ProductRequest productRequest)
    {
        var validationResult = await _productRequestValidator.ValidateAsync(productRequest);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var productEntity = await _repository.GetByIdAsync(id);
        if (productEntity is null)
            throw new ApplicationException($"Product with ID {id} not found.");

        _mapper.Map(productRequest, productEntity);
        productEntity = await _repository.UpdateAsync(productEntity);
        return _mapper.Map<ProductResponse>(productEntity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var productEntity = await _repository.GetByIdAsync(id);

        if (productEntity is null)
            throw new ApplicationException($"Product with ID {id} not found.");

        await _repository.DeleteByIdAsync(id);
    }
}

