using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;
using Ca.Backend.Test.Application.Services.Interfaces;
using Ca.Backend.Test.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Ca.Backend.Test.API.Models.Response.Api;

namespace Ca.Backend.Test.API.Tests;
public class CustomerControllerTests
{
    private readonly Mock<ICustomerServices> _mockCustomerServices;
    private readonly CustomerController _controller;

    public CustomerControllerTests()
    {
        _mockCustomerServices = new Mock<ICustomerServices>();
        _controller = new CustomerController(_mockCustomerServices.Object);
    }

    [Fact]
    public async Task CreateCustomerAsync_ReturnsOkResult_WithCustomerResponse()
    {
        // Arrange
        var request = new CustomerRequest
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Address = "123 Main St"
        };
        var response = new CustomerResponse
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            Email = "john.doe@example.com",
            Address = "123 Main St"
        };
        _mockCustomerServices.Setup(s => s.CreateAsync(request)).ReturnsAsync(response);

        // Act
        var result = await _controller.CreateCustomerAsync(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GenericHttpResponse<CustomerResponse>>(okResult.Value).Data;
        Assert.Equal(response.Id, returnValue.Id);
        Assert.Equal(response.Name, returnValue.Name);
        Assert.Equal(response.Email, returnValue.Email);
        Assert.Equal(response.Address, returnValue.Address);
    }

    [Fact]
    public async Task GetCustomerByIdAsync_ReturnsOkResult_WithCustomerResponse()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var response = new CustomerResponse
        {
            Id = customerId,
            Name = "Jane Doe",
            Email = "jane.doe@example.com",
            Address = "456 Elm St"
        };
        _mockCustomerServices.Setup(s => s.GetByIdAsync(customerId)).ReturnsAsync(response);

        // Act
        var result = await _controller.GetCustomerByIdAsync(customerId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GenericHttpResponse<CustomerResponse>>(okResult.Value).Data;
        Assert.Equal(response.Id, returnValue.Id);
        Assert.Equal(response.Name, returnValue.Name);
        Assert.Equal(response.Email, returnValue.Email);
        Assert.Equal(response.Address, returnValue.Address);
    }

    [Fact]
    public async Task GetAllCustomersAsync_ReturnsOkResult_WithListOfCustomerResponse()
    {
        // Arrange
        var response = new List<CustomerResponse>
        {
            new CustomerResponse { Id = Guid.NewGuid(), Name = "Customer 1", Email = "customer1@example.com", Address = "123 Maple St" },
            new CustomerResponse { Id = Guid.NewGuid(), Name = "Customer 2", Email = "customer2@example.com", Address = "789 Oak St" }
        };
        _mockCustomerServices.Setup(s => s.GetAllAsync()).ReturnsAsync(response);

        // Act
        var result = await _controller.GetAllCustomersAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GenericHttpResponse<IEnumerable<CustomerResponse>>>(okResult.Value).Data;
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public async Task UpdateCustomerAsync_ReturnsOkResult_WithUpdatedCustomerResponse()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var request = new CustomerRequest
        {
            Name = "Updated Customer",
            Email = "updated.customer@example.com",
            Address = "123 Updated St"
        };
        var response = new CustomerResponse
        {
            Id = customerId,
            Name = "Updated Customer",
            Email = "updated.customer@example.com",
            Address = "123 Updated St"
        };
        _mockCustomerServices.Setup(s => s.UpdateAsync(customerId, request)).ReturnsAsync(response);

        // Act
        var result = await _controller.UpdateCustomerAsync(customerId, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GenericHttpResponse<CustomerResponse>>(okResult.Value).Data;
        Assert.Equal(response.Id, returnValue.Id);
        Assert.Equal(response.Name, returnValue.Name);
        Assert.Equal(response.Email, returnValue.Email);
        Assert.Equal(response.Address, returnValue.Address);
    }

    [Fact]
    public async Task DeleteCustomerAsync_ReturnsNoContent()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        _mockCustomerServices.Setup(s => s.DeleteByIdAsync(customerId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteCustomerAsync(customerId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}

