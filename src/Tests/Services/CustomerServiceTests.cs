namespace Tests.Services;

public class CustomerServiceTests
{
    [Fact]
    public async Task WhenGettingAll_ThenAllReturn()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new CustomerService(repositoryWrapperMock.Object, mapper, new CustomerValidator());

        var result = await service.GetAllAsync<Customer>(new Pagination(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotNull(result.Value);
        Assert.Equal(3, result.Value.Count);
    }

    [Fact]
    public async Task GivenExistingId_WhenGettingById_ThenReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new CustomerService(repositoryWrapperMock.Object, mapper, new CustomerValidator());

        var result = await service.GetByIdAsync<Customer>(MockICustomerRepository.Data[2].Id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.IsAssignableFrom<SuccessServiceResult<Customer>>(result);
        Assert.NotNull(result.Value);
        Assert.Equal(MockICustomerRepository.Data[2].Id, result.Value.Id);
        Assert.Equal(MockICustomerRepository.Data[2].Name, result.Value.Name);
        Assert.Equal(MockICustomerRepository.Data[2].Email, result.Value.Email);
        Assert.Equal(MockICustomerRepository.Data[2].Address, result.Value.Address);
    }

    [Fact]
    public async Task GivenNonExistingId_WhenGettingById_ThenNotFoundReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new CustomerService(repositoryWrapperMock.Object, mapper, new CustomerValidator());

        var result = await service.GetByIdAsync<Customer>(new Guid("86CFA85E-035A-454A-BC6F-AD0B5B8CD8E7"), CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.IsAssignableFrom<NotFoundServiceResult>(result.Result);
    }

    [Fact]
    public async Task GivenValidData_WhenCreating_ThenReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new CustomerService(repositoryWrapperMock.Object, mapper, new CustomerValidator());

        var dto = new CustomerDTO(Name: "Test", Email: "test@test.com", Address: "test");

        var result = await service.CreateAsync(dto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.IsAssignableFrom<SuccessServiceResult<Customer>>(result);
        Assert.NotNull(result.Value);
        Assert.NotEqual(Guid.Empty, result.Value.Id);
        Assert.Equal(dto.Name, result.Value.Name);
        Assert.Equal(dto.Email, result.Value.Email);
        Assert.Equal(dto.Address, result.Value.Address);
    }

    [Fact]
    public async Task GivenInvalidData_WhenCreating_ThenValidationErrorReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new CustomerService(repositoryWrapperMock.Object, mapper, new CustomerValidator());

        var dto = new CustomerDTO(Name: "", Email: "", Address: "");

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
        var service = new CustomerService(repositoryWrapperMock.Object, mapper, new CustomerValidator());

        var dto = new CustomerDTO(Name: "Test", Email: "test@test.com", Address: "test");

        var result = await service.UpdateAsync(MockICustomerRepository.Data[1].Id, dto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.IsAssignableFrom<SuccessServiceResult>(result.Result);
    }

    [Fact]
    public async Task GivenNonExistingId_WhenUpdating_ThenNotFoundReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new CustomerService(repositoryWrapperMock.Object, mapper, new CustomerValidator());

        var dto = new CustomerDTO(Name: "Test", Email: "test@test.com", Address: "test");

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
        var service = new CustomerService(repositoryWrapperMock.Object, mapper, new CustomerValidator());

        var dto = new CustomerDTO(Name: "", Email: "", Address: "");

        var result = await service.UpdateAsync(MockICustomerRepository.Data[0].Id, dto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.IsAssignableFrom<ValidationErrorServiceResult>(result.Result);
    }

    [Fact]
    public async Task GivenExistingId_WhenDeleting_ThenReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new CustomerService(repositoryWrapperMock.Object, mapper, new CustomerValidator());

        var result = await service.DeleteAsync(MockICustomerRepository.Data[1].Id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.IsAssignableFrom<SuccessServiceResult>(result.Result);
    }

    [Fact]
    public async Task GivenNonExistingId_WhenDeleting_ThenNotFoundReturns()
    {
        var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
        var mapper = AutoMapper.GetMapper();
        var service = new CustomerService(repositoryWrapperMock.Object, mapper, new CustomerValidator());

        var result = await service.DeleteAsync(new Guid("86CFA85E-035A-454A-BC6F-AD0B5B8CD8E7"), CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.IsAssignableFrom<NotFoundServiceResult>(result.Result);
    }
}
