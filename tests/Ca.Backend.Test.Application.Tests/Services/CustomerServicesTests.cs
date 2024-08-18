using AutoMapper;
using Ca.Backend.Test.Application.Mappings;
using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Services;
using Ca.Backend.Test.Application.Services.Interfaces;
using Ca.Backend.Test.Domain.Entities;
using Ca.Backend.Test.Infra.Data.Repository.Interfaces;
using FluentAssertions;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ca.Backend.Test.Application.Tests.Services;
public class CustomerServicesTests
{
    private ICustomerServices _customerService;
    private Mock<IGenericRepository<CustomerEntity>> _mockRepository;
    private IMapper _mapper;
    private Mock<IValidator<CustomerRequest>> _mockValidator;

    public CustomerServicesTests()
    {
        _mockRepository = new Mock<IGenericRepository<CustomerEntity>>();
        _mockValidator = new Mock<IValidator<CustomerRequest>>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        _mapper = mapperConfig.CreateMapper();

        _customerService = new CustomerServices(_mockRepository.Object, _mapper, _mockValidator.Object);
    }

    [Fact]
    public async Task CreateAsync_ValidCustomerRequest_ReturnsCustomerResponse()
    {
        // Arrange
        var customerRequest = new CustomerRequest
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Address = "123 Main St"
        };

        _mockValidator.Setup(v => v.ValidateAsync(customerRequest, default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var createdCustomerEntity = new CustomerEntity
        {
            Id = Guid.NewGuid(),
            Name = customerRequest.Name,
            Email = customerRequest.Email,
            Address = customerRequest.Address
        };

        _mockRepository.Setup(r => r.CreateAsync(It.IsAny<CustomerEntity>()))
            .ReturnsAsync(createdCustomerEntity);

        // Act
        var result = await _customerService.CreateAsync(customerRequest);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(createdCustomerEntity.Id);
        result.Name.Should().Be(customerRequest.Name);
        result.Email.Should().Be(customerRequest.Email);
        result.Address.Should().Be(customerRequest.Address);
    }

    [Fact]
    public async Task CreateAsync_InvalidCustomerRequest_ThrowsValidationException()
    {
        // Arrange
        var customerRequest = new CustomerRequest();

        var validationResult = new FluentValidation.Results.ValidationResult();
        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Name", "Name is required"));
        _mockValidator.Setup(v => v.ValidateAsync(customerRequest, default))
            .ReturnsAsync(validationResult);

        // Act
        Func<Task> action = async () => await _customerService.CreateAsync(customerRequest);

        // Assert
        await action.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task GetByIdAsync_ExistingCustomerId_ReturnsCustomerResponse()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var existingCustomerEntity = new CustomerEntity
        {
            Id = customerId,
            Name = "Jane Doe",
            Email = "jane.doe@example.com",
            Address = "456 Oak St"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(customerId))
            .ReturnsAsync(existingCustomerEntity);

        // Act
        var result = await _customerService.GetByIdAsync(customerId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(existingCustomerEntity.Id);
        result.Name.Should().Be(existingCustomerEntity.Name);
        result.Email.Should().Be(existingCustomerEntity.Email);
        result.Address.Should().Be(existingCustomerEntity.Address);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingCustomerId_ThrowsApplicationException()
    {
        // Arrange
        var customerId = Guid.NewGuid();

        _mockRepository.Setup(r => r.GetByIdAsync(customerId))
            .ReturnsAsync((CustomerEntity)null);

        // Act
        Func<Task> action = async () => await _customerService.GetByIdAsync(customerId);

        // Assert
        await action.Should().ThrowAsync<ApplicationException>()
            .WithMessage($"Customer with ID {customerId} not found.");
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllCustomers()
    {
        // Arrange
        var customersEntities = new List<CustomerEntity>
        {
            new CustomerEntity { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com", Address = "123 Main St" },
            new CustomerEntity { Id = Guid.NewGuid(), Name = "Jane Doe", Email = "jane.doe@example.com", Address = "456 Oak St" }
        };

        _mockRepository.Setup(r => r.GetAllAsync())
            .ReturnsAsync(customersEntities);

        // Act
        var result = await _customerService.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.First().Name.Should().Be("John Doe");
        result.Last().Name.Should().Be("Jane Doe");
    }

    [Fact]
    public async Task GetAllAsync_NoCustomers_ReturnsEmptyList()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<CustomerEntity>());

        // Act
        var result = await _customerService.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task UpdateAsync_ValidCustomerRequest_ReturnsUpdatedCustomerResponse()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var customerRequest = new CustomerRequest
        {
            Name = "Updated Name",
            Email = "updated.email@example.com",
            Address = "789 Pine St"
        };

        _mockValidator.Setup(v => v.ValidateAsync(customerRequest, default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var existingCustomerEntity = new CustomerEntity
        {
            Id = customerId,
            Name = "Old Name",
            Email = "old.email@example.com",
            Address = "123 Old St"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(customerId))
            .ReturnsAsync(existingCustomerEntity);

        var updatedCustomerEntity = new CustomerEntity
        {
            Id = customerId,
            Name = customerRequest.Name,
            Email = customerRequest.Email,
            Address = customerRequest.Address
        };

        _mockRepository.Setup(r => r.UpdateAsync(existingCustomerEntity))
            .ReturnsAsync(updatedCustomerEntity);

        // Act
        var result = await _customerService.UpdateAsync(customerId, customerRequest);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(customerId);
        result.Name.Should().Be(customerRequest.Name);
        result.Email.Should().Be(customerRequest.Email);
        result.Address.Should().Be(customerRequest.Address);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingCustomerId_ThrowsApplicationException()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var customerRequest = new CustomerRequest
        {
            Name = "Updated Name",
            Email = "updated.email@example.com",
            Address = "789 Pine St"
        };

        _mockValidator.Setup(v => v.ValidateAsync(customerRequest, default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mockRepository.Setup(r => r.GetByIdAsync(customerId))
            .ReturnsAsync((CustomerEntity)null);

        // Act
        Func<Task> action = async () => await _customerService.UpdateAsync(customerId, customerRequest);

        // Assert
        await action.Should().ThrowAsync<ApplicationException>()
            .WithMessage($"Customer with ID {customerId} not found.");
    }

    [Fact]
    public async Task DeleteByIdAsync_ExistingCustomerId_CallsRepositoryDelete()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var existingCustomerEntity = new CustomerEntity
        {
            Id = customerId,
            Name = "Jane Doe",
            Email = "jane.doe@example.com",
            Address = "456 Oak St"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(customerId))
            .ReturnsAsync(existingCustomerEntity);

        // Act
        await _customerService.DeleteByIdAsync(customerId);

        // Assert
        _mockRepository.Verify(r => r.DeleteByIdAsync(customerId), Times.Once);
    }

    [Fact]
    public async Task DeleteByIdAsync_NonExistingCustomerId_ThrowsApplicationException()
    {
        // Arrange
        var customerId = Guid.NewGuid();

        _mockRepository.Setup(r => r.GetByIdAsync(customerId))
            .ReturnsAsync((CustomerEntity)null);

        // Act
        Func<Task> action = async () => await _customerService.DeleteByIdAsync(customerId);

        // Assert
        await action.Should().ThrowAsync<ApplicationException>()
            .WithMessage($"Customer with ID {customerId} not found.");
    }
}

