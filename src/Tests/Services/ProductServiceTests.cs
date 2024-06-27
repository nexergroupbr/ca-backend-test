namespace Tests.Services;

public class ProductServiceTests
{
    [Fact]
    public async Task WhenGettingAll_ThenAllReturn()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new ProductService(repositoryWrapperMock.Object, mapper, new ProductValidator());

        var result = await service.GetAllAsync<Product>(new Pagination(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotNull(result.Value);
        Assert.Equal(3, result.Value.Count);
    }

    [Fact]
    public async Task GivenExistingId_WhenGettingById_ThenReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new ProductService(repositoryWrapperMock.Object, mapper, new ProductValidator());

        var result = await service.GetByIdAsync<Product>(MockIProductRepository.Data[2].Id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.IsAssignableFrom<SuccessServiceResult<Product>>(result);
        Assert.NotNull(result.Value);
        Assert.Equal(MockIProductRepository.Data[2].Id, result.Value.Id);
        Assert.Equal(MockIProductRepository.Data[2].Name, result.Value.Name);
    }

    [Fact]
    public async Task GivenNonExistingId_WhenGettingById_ThenNotFoundReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new ProductService(repositoryWrapperMock.Object, mapper, new ProductValidator());

        var result = await service.GetByIdAsync<Product>(new Guid("86CFA85E-035A-454A-BC6F-AD0B5B8CD8E7"), CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.IsAssignableFrom<NotFoundServiceResult>(result.Result);
    }

    [Fact]
    public async Task GivenValidData_WhenCreating_ThenReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new ProductService(repositoryWrapperMock.Object, mapper, new ProductValidator());

        var dto = new ProductDTO(Name: "Test");

        var result = await service.CreateAsync(dto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.IsAssignableFrom<SuccessServiceResult<Product>>(result);
        Assert.NotNull(result.Value);
        Assert.NotEqual(Guid.Empty, result.Value.Id);
        Assert.Equal(dto.Name, result.Value.Name);
    }

    [Fact]
    public async Task GivenInvalidData_WhenCreating_ThenValidationErrorReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new ProductService(repositoryWrapperMock.Object, mapper, new ProductValidator());

        var dto = new ProductDTO(Name: "");

        var result = await service.CreateAsync(dto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.IsAssignableFrom<ValidationErrorServiceResult>(result.Result);
    }

    [Fact]
    public async Task GivenValidData_WhenUpdating_ThenReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new ProductService(repositoryWrapperMock.Object, mapper, new ProductValidator());

        var dto = new ProductDTO(Name: "Test");

        var result = await service.UpdateAsync(MockIProductRepository.Data[1].Id, dto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.IsAssignableFrom<SuccessServiceResult>(result.Result);
    }

    [Fact]
    public async Task GivenNonExistingId_WhenUpdating_ThenNotFoundReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new ProductService(repositoryWrapperMock.Object, mapper, new ProductValidator());

        var dto = new ProductDTO(Name: "Test");

        var result = await service.UpdateAsync(new Guid("86CFA85E-035A-454A-BC6F-AD0B5B8CD8E7"), dto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.IsAssignableFrom<NotFoundServiceResult>(result.Result);
    }

    [Fact]
    public async Task GivenInvalidData_WhenUpdating_ThenValidationErrorReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new ProductService(repositoryWrapperMock.Object, mapper, new ProductValidator());

        var dto = new ProductDTO(Name: "");

        var result = await service.UpdateAsync(MockIProductRepository.Data[0].Id, dto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.IsAssignableFrom<ValidationErrorServiceResult>(result.Result);
    }

    [Fact]
    public async Task GivenExistingId_WhenDeleting_ThenReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new ProductService(repositoryWrapperMock.Object, mapper, new ProductValidator());

        var result = await service.DeleteAsync(MockIProductRepository.Data[1].Id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.IsAssignableFrom<SuccessServiceResult>(result.Result);
    }

    [Fact]
    public async Task GivenNonExistingId_WhenDeleting_ThenNotFoundReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new ProductService(repositoryWrapperMock.Object, mapper, new ProductValidator());

        var result = await service.DeleteAsync(new Guid("86CFA85E-035A-454A-BC6F-AD0B5B8CD8E7"), CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.IsAssignableFrom<NotFoundServiceResult>(result.Result);
    }
}
