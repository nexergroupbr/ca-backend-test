namespace Domain.ServiceResults;

public interface ICreatedServiceResult
{
    public string RouteName { get; }
    public object RouteValues { get; }
    public object? Value { get; }
}

public record CreatedServiceResult<T>
    : SuccessServiceResult<T>,
      ICreatedServiceResult
    where T : BaseEntity
{
    public CreatedServiceResult(string createdRouteName, ServiceResult<T> result)
        : base(result.Value)
    {
        RouteName = createdRouteName;
        RouteValues = new { id = result.Value?.Id };
    }

    public string RouteName { get; }
    public object RouteValues { get; }
    object? ICreatedServiceResult.Value { get => Value; }

    public static CreatedServiceResult<T> Create(string createdRouteName, ServiceResult<T> result)
    {
        return new CreatedServiceResult<T>(createdRouteName, result);
    }
}
