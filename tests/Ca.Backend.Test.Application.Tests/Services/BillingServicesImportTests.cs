using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ca.Backend.Test.Application.Mappings;
using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response.Api;
using Ca.Backend.Test.Application.Services;
using Ca.Backend.Test.Application.Services.Interfaces;
using Ca.Backend.Test.Domain.Entities;
using Ca.Backend.Test.Infra.Data.Repository.Interfaces;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Ca.Backend.Test.Application.Tests.Services;
public class BillingServicesImportTests
{
    private readonly IBillingServices _billingService;
    private readonly Mock<IGenericRepository<BillingEntity>> _mockRepository;
    private readonly Mock<IGenericRepository<CustomerEntity>> _mockCustomerRepository;
    private readonly Mock<IGenericRepository<ProductEntity>> _mockProductRepository;
    private readonly IMapper _mapper;
    private readonly Mock<IValidator<BillingRequest>> _mockValidator;
    private readonly Mock<HttpClient> _mockHttpClient;

    public BillingServicesImportTests()
    {
        _mockRepository = new Mock<IGenericRepository<BillingEntity>>();
        _mockCustomerRepository = new Mock<IGenericRepository<CustomerEntity>>();
        _mockProductRepository = new Mock<IGenericRepository<ProductEntity>>();
        _mockValidator = new Mock<IValidator<BillingRequest>>();
        _mockHttpClient = new Mock<HttpClient>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        _mapper = mapperConfig.CreateMapper();

        _billingService = new BillingServices(
            _mockRepository.Object,
            _mockCustomerRepository.Object,
            _mapper,
            _mockValidator.Object,
            _mockProductRepository.Object,
            _mockHttpClient.Object
        );
    }

    [Fact]
    public async Task ImportBillingFromExternalApiAsync_ValidApiResponse_ImportsBillingSuccessfully()
    {
        // Arrange
        var billingApiResponse = new BillingApiResponse
        {
            InvoiceNumber = "INV-001",
            Customer = new CustomerApiResponse
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john.doe@example.com",
                Address = "123 Main St"
            },
            Date = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(30),
            TotalAmount = 100,
            Currency = "USD",
            Lines = new List<LineApiResponse>
            {
                new LineApiResponse
                {
                    ProductId = Guid.NewGuid(),
                    Description = "Test Product",
                    Quantity = 1,
                    UnitPrice = 100,
                    Subtotal = 100
                }
            }
        };

        var billingApiResponseJson = JsonSerializer.Serialize(new List<BillingApiResponse> { billingApiResponse });

        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(billingApiResponseJson)
        };

        _mockHttpClient
            .Setup(client => client.SendAsync(
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(httpResponseMessage);

        var customerEntity = new CustomerEntity { Id = billingApiResponse.Customer.Id };
        _mockCustomerRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(customerEntity);

        var productEntity = new ProductEntity { Id = billingApiResponse.Lines[0].ProductId, Description = "Test Product" };
        _mockProductRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(productEntity);

        _mockRepository.Setup(r => r.CreateAsync(It.IsAny<BillingEntity>()))
                        .ReturnsAsync((BillingEntity entity) => entity);

        // Act
        await _billingService.ImportBillingFromExternalApiAsync();

        // Assert
        _mockRepository.Verify(r => r.CreateAsync(It.IsAny<BillingEntity>()), Times.Once);
    }

    [Fact]
    public async Task ImportBillingFromExternalApiAsync_InvalidCustomer_ThrowsApplicationException()
    {
        // Arrange
        var billingApiResponse = new BillingApiResponse
        {
            InvoiceNumber = "INV-001",
            Customer = new CustomerApiResponse
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john.doe@example.com",
                Address = "123 Main St"
            },
            Date = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(30),
            TotalAmount = 100,
            Currency = "USD",
            Lines = new List<LineApiResponse>
            {
                new LineApiResponse
                {
                    ProductId = Guid.NewGuid(),
                    Description = "Test Product",
                    Quantity = 1,
                    UnitPrice = 100,
                    Subtotal = 100
                }
            }
        };

        var billingApiResponseJson = JsonSerializer.Serialize(new List<BillingApiResponse> { billingApiResponse });

        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(billingApiResponseJson)
        };

        _mockHttpClient
            .Setup(client => client.SendAsync(
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(httpResponseMessage);

        _mockCustomerRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                            .ReturnsAsync((CustomerEntity)null);

        // Act
        Func<Task> action = async () => await _billingService.ImportBillingFromExternalApiAsync();

        // Assert
        await action.Should().ThrowAsync<ApplicationException>()
                    .WithMessage($"Customer with ID {billingApiResponse.Customer.Id} not found.");
    }

    [Fact]
    public async Task ImportBillingFromExternalApiAsync_InvalidProduct_ThrowsApplicationException()
    {
        // Arrange
        var billingApiResponse = new BillingApiResponse
        {
            InvoiceNumber = "INV-001",
            Customer = new CustomerApiResponse
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john.doe@example.com",
                Address = "123 Main St"
            },
            Date = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(30),
            TotalAmount = 100,
            Currency = "USD",
            Lines = new List<LineApiResponse>
            {
                new LineApiResponse
                {
                    ProductId = Guid.NewGuid(),
                    Description = "Test Product",
                    Quantity = 1,
                    UnitPrice = 100,
                    Subtotal = 100
                }
            }
        };

        var billingApiResponseJson = JsonSerializer.Serialize(new List<BillingApiResponse> { billingApiResponse });

        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(billingApiResponseJson)
        };

        _mockHttpClient
            .Setup(client => client.SendAsync(
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(httpResponseMessage);

        var customerEntity = new CustomerEntity { Id = billingApiResponse.Customer.Id };
        _mockCustomerRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(customerEntity);

        _mockProductRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync((ProductEntity)null);

        // Act
        Func<Task> action = async () => await _billingService.ImportBillingFromExternalApiAsync();

        // Assert
        await action.Should().ThrowAsync<ApplicationException>()
                    .WithMessage($"Product with ID {billingApiResponse.Lines[0].ProductId} not found.");
    }
}

