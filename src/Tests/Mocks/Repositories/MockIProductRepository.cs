using Moq;

namespace Tests.Mocks.Repositories;

internal static class MockIProductRepository
{
    internal static readonly List<Product> Data = [
        new() { Id = new Guid("895E0D00-280C-4A13-959A-8D4C482BCB4F"), Name = "Product 1" },
        new() { Id = new Guid("031E3256-80B6-4416-B4A3-C79765DE2BC6"), Name = "Product 2" },
        new() { Id = new Guid("112E039E-91DB-4FA5-937E-D2A4FCBDE56C"), Name = "Product 3" }
    ];

    private static Mock<IProductRepository> Initialize()
    {
        var mock = new Mock<IProductRepository>();

        mock.Setup(m => m.ExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns((Guid id, CancellationToken cancellationToken) => Task.FromResult(Data.Find(q => q.Id == id) is not null));

        mock.Setup(m => m.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        mock.Setup(m => m.GetByIdAsync<Product>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns((Guid id, CancellationToken cancellationToken) => Task.FromResult(Data.Find(q => q.Id == id)));

        mock.Setup(m => m.GetAllAsync<Product>(It.IsAny<Pagination>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Data));

        mock.Setup(m => m.Create(It.IsAny<Product>()));

        mock.Setup(m => m.Update(It.IsAny<Product>()));

        return mock;
    }

    private static Mock<IProductRepository>? mock;
    internal static Mock<IProductRepository> GetMock()
        => mock ??= Initialize();
}
