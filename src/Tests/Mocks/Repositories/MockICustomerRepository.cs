using Moq;

namespace Tests.Mocks.Repositories;

internal static class MockICustomerRepository
{
    internal static readonly List<Customer> Data = [
        new() { Id = new Guid("A61A2474-55CD-45A9-B6BD-090E38958B80"), Name = "Customer 1", Email = "customer1@email.com", Address = "Customer 1 Address" },
        new() { Id = new Guid("A7E47F15-64B6-48B7-88BD-F16DC191D5FF"), Name = "Customer 2", Email = "customer2@email.com", Address = "Customer 2 Address" },
        new() { Id = new Guid("082B844D-E5E5-45A1-9E8F-63518298CF2E"), Name = "Customer 3", Email = "customer3@email.com", Address = "Customer 3 Address" }
    ];

    private static Mock<ICustomerRepository> Initialize()
    {
        var mock = new Mock<ICustomerRepository>();

        mock.Setup(m => m.ExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns((Guid id, CancellationToken cancellationToken) => Task.FromResult(Data.Find(q => q.Id == id) is not null));

        mock.Setup(m => m.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        mock.Setup(m => m.GetByIdAsync<Customer>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns((Guid id, CancellationToken cancellationToken) => Task.FromResult(Data.Find(q => q.Id == id)));

        mock.Setup(m => m.GetAllAsync<Customer>(It.IsAny<Pagination>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Data));

        mock.Setup(m => m.Create(It.IsAny<Customer>()));

        mock.Setup(m => m.Update(It.IsAny<Customer>()));

        return mock;
    }

    private static Mock<ICustomerRepository>? mock;
    internal static Mock<ICustomerRepository> GetMock()
        => mock ??= Initialize();
}
