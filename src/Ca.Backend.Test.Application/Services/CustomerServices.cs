using AutoMapper;
using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;
using Ca.Backend.Test.Application.Services.Interfaces;
using Ca.Backend.Test.Domain.Entities;
using Ca.Backend.Test.Infra.Data.Repository.Interfaces;
using FluentValidation;

namespace Ca.Backend.Test.Application.Services;
public class CustomerServices : ICustomerServices
{
    private readonly IGenericRepository<CustomerEntity> _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<CustomerRequest> _customerRequestValidator;

    public CustomerServices(IGenericRepository<CustomerEntity> repository, IMapper mapper, 
                            IValidator<CustomerRequest> customerRequestValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _customerRequestValidator = customerRequestValidator;
    }

    public async Task<CustomerResponse> CreateAsync(CustomerRequest customerRequest)
    {
        var validationResult = await _customerRequestValidator.ValidateAsync(customerRequest);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var customerEntity = _mapper.Map<CustomerEntity>(customerRequest);
        customerEntity = await _repository.CreateAsync(customerEntity);
        return _mapper.Map<CustomerResponse>(customerEntity);
    }

    public async Task<CustomerResponse> GetByIdAsync(Guid id)
    {
        var customerEntity = await _repository.GetByIdAsync(id);

        if (customerEntity is null)
            throw new ApplicationException($"Customer with ID {id} not found.");

        return _mapper.Map<CustomerResponse>(customerEntity);
    }

    public async Task<IEnumerable<CustomerResponse>> GetAllAsync()
    {
        var customersEntities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerResponse>>(customersEntities);
    }

    public async Task<CustomerResponse> UpdateAsync(Guid id, CustomerRequest customerRequest)
    {
        var validationResult = await _customerRequestValidator.ValidateAsync(customerRequest);
        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors);

        var customerEntity = await _repository.GetByIdAsync(id);
        if (customerEntity is null)
            throw new ApplicationException($"Customer with ID {id} not found.");

        _mapper.Map(customerRequest, customerEntity);
        customerEntity = await _repository.UpdateAsync(customerEntity);
        return _mapper.Map<CustomerResponse>(customerEntity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var customerEntity = await _repository.GetByIdAsync(id);
        if (customerEntity is null)
            throw new ApplicationException($"Customer with ID {id} not found.");

        await _repository.DeleteByIdAsync(id);
    }
}

