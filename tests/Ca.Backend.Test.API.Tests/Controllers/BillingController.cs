using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;
using Ca.Backend.Test.Application.Services.Interfaces;
using Ca.Backend.Test.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Ca.Backend.Test.API.Models.Response.Api;

namespace Ca.Backend.Test.API.Tests;
public class BillingControllerTests
{
    private readonly Mock<IBillingServices> _mockBillingServices;
    private readonly BillingController _controller;

    public BillingControllerTests()
    {
        _mockBillingServices = new Mock<IBillingServices>();
        _controller = new BillingController(_mockBillingServices.Object);
    }

    [Fact]
    public async Task ImportBillingAsync_ReturnsNoContent()
    {
        // Arrange
        _mockBillingServices.Setup(s => s.ImportBillingFromExternalApiAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.ImportBillingAsync();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task CreateBillingAsync_ReturnsOkResult_WithBillingResponse()
    {
        // Arrange
        var request = new BillingRequest
        {
            CustomerId = Guid.NewGuid(),
            InvoiceNumber = "INV-1001",
            Date = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(30),
            TotalAmount = 1000.00m,
            Currency = "USD",
            Lines = new List<BillingLineRequest> { new BillingLineRequest() }
        };
        var response = new BillingResponse
        {
            Id = Guid.NewGuid(),
            InvoiceNumber = "INV-1001",
            Date = request.Date,
            DueDate = request.DueDate,
            TotalAmount = request.TotalAmount,
            Currency = request.Currency,
            Lines = new List<BillingLineResponse> { new BillingLineResponse() }
        };
        _mockBillingServices.Setup(s => s.CreateAsync(request)).ReturnsAsync(response);

        // Act
        var result = await _controller.CreateBillingAsync(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GenericHttpResponse<BillingResponse>>(okResult.Value).Data;
        Assert.Equal(response.Id, returnValue.Id);
        Assert.Equal(response.InvoiceNumber, returnValue.InvoiceNumber);
        Assert.Equal(response.Date, returnValue.Date);
        Assert.Equal(response.DueDate, returnValue.DueDate);
        Assert.Equal(response.TotalAmount, returnValue.TotalAmount);
        Assert.Equal(response.Currency, returnValue.Currency);
        Assert.Equal(response.Lines.Count, returnValue.Lines.Count);
    }

    [Fact]
    public async Task GetBillingByIdAsync_ReturnsOkResult_WithBillingResponse()
    {
        // Arrange
        var billingId = Guid.NewGuid();
        var response = new BillingResponse
        {
            Id = billingId,
            InvoiceNumber = "INV-1002",
            Date = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(30),
            TotalAmount = 2000.00m,
            Currency = "EUR",
            Lines = new List<BillingLineResponse> { new BillingLineResponse() }
        };
        _mockBillingServices.Setup(s => s.GetByIdAsync(billingId)).ReturnsAsync(response);

        // Act
        var result = await _controller.GetBillingByIdAsync(billingId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GenericHttpResponse<BillingResponse>>(okResult.Value).Data;
        Assert.Equal(response.Id, returnValue.Id);
        Assert.Equal(response.InvoiceNumber, returnValue.InvoiceNumber);
        Assert.Equal(response.Date, returnValue.Date);
        Assert.Equal(response.DueDate, returnValue.DueDate);
        Assert.Equal(response.TotalAmount, returnValue.TotalAmount);
        Assert.Equal(response.Currency, returnValue.Currency);
        Assert.Equal(response.Lines.Count, returnValue.Lines.Count);
    }

    [Fact]
    public async Task GetAllBillingsAsync_ReturnsOkResult_WithListOfBillingResponse()
    {
        // Arrange
        var response = new List<BillingResponse>
        {
            new BillingResponse
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = "INV-1003",
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                TotalAmount = 3000.00m,
                Currency = "GBP",
                Lines = new List<BillingLineResponse> { new BillingLineResponse() }
            },
            new BillingResponse
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = "INV-1004",
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                TotalAmount = 4000.00m,
                Currency = "AUD",
                Lines = new List<BillingLineResponse> { new BillingLineResponse() }
            }
        };
        _mockBillingServices.Setup(s => s.GetAllAsync()).ReturnsAsync(response);

        // Act
        var result = await _controller.GetAllBillingsAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GenericHttpResponse<IEnumerable<BillingResponse>>>(okResult.Value).Data;
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public async Task UpdateBillingAsync_ReturnsOkResult_WithUpdatedBillingResponse()
    {
        // Arrange
        var billingId = Guid.NewGuid();
        var request = new BillingRequest
        {
            CustomerId = Guid.NewGuid(),
            InvoiceNumber = "INV-1005",
            Date = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(30),
            TotalAmount = 5000.00m,
            Currency = "CAD",
            Lines = new List<BillingLineRequest> { new BillingLineRequest() }
        };
        var response = new BillingResponse
        {
            Id = billingId,
            InvoiceNumber = "INV-1005",
            Date = request.Date,
            DueDate = request.DueDate,
            TotalAmount = request.TotalAmount,
            Currency = request.Currency,
            Lines = new List<BillingLineResponse> { new BillingLineResponse() }
        };
        _mockBillingServices.Setup(s => s.UpdateAsync(billingId, request)).ReturnsAsync(response);

        // Act
        var result = await _controller.UpdateBillingAsync(billingId, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GenericHttpResponse<BillingResponse>>(okResult.Value).Data;
        Assert.Equal(response.Id, returnValue.Id);
        Assert.Equal(response.InvoiceNumber, returnValue.InvoiceNumber);
        Assert.Equal(response.Date, returnValue.Date);
        Assert.Equal(response.DueDate, returnValue.DueDate);
        Assert.Equal(response.TotalAmount, returnValue.TotalAmount);
        Assert.Equal(response.Currency, returnValue.Currency);
        Assert.Equal(response.Lines.Count, returnValue.Lines.Count);
    }

    [Fact]
    public async Task DeleteBillingAsync_ReturnsNoContent()
    {
        // Arrange
        var billingId = Guid.NewGuid();
        _mockBillingServices.Setup(s => s.DeleteByIdAsync(billingId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteBillingAsync(billingId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}

