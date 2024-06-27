namespace Infraestructure.Repositories;

public abstract class EntityRepository<TEntity>
    : BaseRepository<TEntity>
    where TEntity : BaseEntity
{
    public EntityRepository(DataContext repositoryContext, IMapper mapper)
        : base(repositoryContext, mapper)
    {

    }

    #region Queries

    protected IQueryable<TEntity> GetByIdQuery(Guid id)
    => GetByConditionQuery(q => q.Id == id);

    #endregion


    public async Task<TModel?> GetByIdAsync<TModel>(Guid id, CancellationToken cancellationToken = default)
        => await FirstOrDefaultAsync<TModel>(GetByIdQuery(id), cancellationToken);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => await GetByIdQuery(id).ExecuteDeleteAsync(cancellationToken);

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => await GetByIdQuery(id).AnyAsync(cancellationToken);
}
