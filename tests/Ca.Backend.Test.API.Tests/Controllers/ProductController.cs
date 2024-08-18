using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;
using Ca.Backend.Test.Application.Services.Interfaces;
using Ca.Backend.Test.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Ca.Backend.Test.API.Models.Response.Api;

namespace Ca.Backend.Test.API.Tests;
public class ProductControllerTests
{
    private readonly Mock<IProductServices> _mockProductServices;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockProductServices = new Mock<IProductServices>();
        _controller = new ProductController(_mockProductServices.Object);
    }

    [Fact]
    public async Task CreateProductAsync_ReturnsOkResult_WithProductResponse()
    {
        // Arrange
        var request = new ProductRequest { Description = "New Product" };
        var response = new ProductResponse { Id = Guid.NewGuid(), Description = "New Product" };
        _mockProductServices.Setup(s => s.CreateAsync(request)).ReturnsAsync(response);

        // Act
        var result = await _controller.CreateProductAsync(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GenericHttpResponse<ProductResponse>>(okResult.Value).Data;
        Assert.Equal(response.Id, returnValue.Id);
        Assert.Equal(response.Description, returnValue.Description);
    }

    [Fact]
    public async Task GetProductByIdAsync_ReturnsOkResult_WithProductResponse()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var response = new ProductResponse { Id = productId, Description = "Existing Product" };
        _mockProductServices.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync(response);

        // Act
        var result = await _controller.GetProductByIdAsync(productId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GenericHttpResponse<ProductResponse>>(okResult.Value).Data;
        Assert.Equal(response.Id, returnValue.Id);
        Assert.Equal(response.Description, returnValue.Description);
    }

    [Fact]
    public async Task GetAllProductsAsync_ReturnsOkResult_WithListOfProductResponse()
    {
        // Arrange
        var response = new List<ProductResponse>
        {
            new ProductResponse { Id = Guid.NewGuid(), Description = "Product 1" },
            new ProductResponse { Id = Guid.NewGuid(), Description = "Product 2" }
        };
        _mockProductServices.Setup(s => s.GetAllAsync()).ReturnsAsync(response);

        // Act
        var result = await _controller.GetAllProductsAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GenericHttpResponse<IEnumerable<ProductResponse>>>(okResult.Value).Data;
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public async Task UpdateProductAsync_ReturnsOkResult_WithUpdatedProductResponse()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var request = new ProductRequest { Description = "Updated Product" };
        var response = new ProductResponse { Id = productId, Description = "Updated Product" };
        _mockProductServices.Setup(s => s.UpdateAsync(productId, request)).ReturnsAsync(response);

        // Act
        var result = await _controller.UpdateProductAsync(productId, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GenericHttpResponse<ProductResponse>>(okResult.Value).Data;
        Assert.Equal(response.Id, returnValue.Id);
        Assert.Equal(response.Description, returnValue.Description);
    }

    [Fact]
    public async Task DeleteProductAsync_ReturnsNoContent()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _mockProductServices.Setup(s => s.DeleteByIdAsync(productId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteProductAsync(productId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}

