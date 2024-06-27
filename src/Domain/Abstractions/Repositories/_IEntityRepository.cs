namespace Domain.Abstractions.Repositories;

public interface IEntityRepository<T>
    : IRepository<T>
{
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TModel?> GetByIdAsync<TModel>(Guid id, CancellationToken cancellationToken = default);
}
