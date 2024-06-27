using System.Linq.Expressions;

namespace Infraestructure.Repositories;

public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
{
    protected DataContext DataContext;
    protected DbSet<TEntity> DbSet;
    protected readonly IMapper Mapper;


    public BaseRepository(DataContext repositoryContext, IMapper mapper)
    {
        DataContext = repositoryContext;
        DbSet = DataContext.Set<TEntity>();
        Mapper = mapper;
    }

    #region Queries

    protected IQueryable<TEntity> GetAllQuery()
        => DbSet.AsNoTracking();

    protected IQueryable<TEntity> GetByConditionQuery(Expression<Func<TEntity, bool>> expression)
        => DbSet.Where(expression).AsNoTracking();

    protected IQueryable<TEntity> OrderQuery(IQueryable<TEntity> source, Pagination pagination)
    {
        pagination ??= new();

        IQueryable<TEntity> query;

        if (!string.IsNullOrWhiteSpace(pagination.OrderBy))
        {
            query = source.OrderBy(pagination.OrderBy, pagination.Asc);
        }
        else
        {
            query = source.OrderByDescending(q => q.CreatedAt);
        }

        return query;
    }

    protected IQueryable<TEntity> PaginateQuery(IQueryable<TEntity> source, Pagination pagination)
    {
        pagination ??= new();

        IQueryable<TEntity> query = source;

        if (pagination.Offset > 0)
        {
            query = query.Skip(pagination.Offset);
        }
        if (pagination.Limit > 0)
        {
            query = query.Take(pagination.Limit.Value);
        }

        return query;
    }

    #endregion


    public void Create(TEntity entity)
        => DbSet.Add(entity);

    public void Update(TEntity entity)
        => DbSet.Update(entity);

    protected async Task<TModel?> FirstOrDefaultAsync<TModel>(IQueryable<TEntity> query, CancellationToken cancellationToken = default)
    {
        if (typeof(TModel) == typeof(TEntity))
        {
            return (TModel?)Convert.ChangeType(await query.FirstOrDefaultAsync(cancellationToken), typeof(TModel));
        }

        var mappedQuery = Mapper.ProjectTo<TModel>(query);
        return await mappedQuery.FirstOrDefaultAsync(cancellationToken);
    }

    protected async Task<List<TModel>> ToListAsync<TModel>(IQueryable<TEntity> query, CancellationToken cancellationToken = default)
    {
        if (typeof(TModel) == typeof(TEntity))
        {
            return ((await query.ToListAsync(cancellationToken)) as List<TModel>)!;
        }

        var mappedQuery = Mapper.ProjectTo<TModel>(query);
        return await mappedQuery.ToListAsync(cancellationToken);
    }


    public async Task<List<TModel>> GetAllAsync<TModel>(Pagination pagination, CancellationToken cancellationToken = default)
    {
        var query = GetAllQuery();

        query = OrderQuery(query, pagination);
        query = PaginateQuery(query, pagination);

        return await ToListAsync<TModel>(query, cancellationToken);
    }
}
