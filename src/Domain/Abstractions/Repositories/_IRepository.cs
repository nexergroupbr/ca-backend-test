namespace Domain.Abstractions.Repositories;

public interface IRepository<T>
{
    void Create(T entity);

    void Update(T entity);

    Task<List<TModel>> GetAllAsync<TModel>(Pagination pagination, CancellationToken cancellationToken = default);
}
