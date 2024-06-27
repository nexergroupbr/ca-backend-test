using Moq;

namespace Tests.Mocks.Repositories;

internal class MockIRepositoryWrapper
{
    private static Mock<IRepositoryWrapper> Initialize()
    {
        var mock = new Mock<IRepositoryWrapper>();

        mock.Setup(m => m.Customer)
            .Returns(() => MockICustomerRepository.GetMock().Object);

        mock.Setup(m => m.Product)
            .Returns(() => MockIProductRepository.GetMock().Object);

        mock.Setup(m => m.OpenTransactionAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        mock.Setup(m => m.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        mock.Setup(m => m.RollbackAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        mock.Setup(m => m.SaveAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        return mock;
    }

    private static Mock<IRepositoryWrapper>? mock;
    internal static Mock<IRepositoryWrapper> GetMock()
        => mock ??= Initialize();
}
