namespace Services;

public abstract class BaseService
{
    protected static ConflictServiceResult Conflict(string message) => new(message);

    protected static ForbiddenServiceResult Forbidden() => new();

    protected static InvalidServiceResult Invalid(string message) => new(message);

    protected static NotFoundServiceResult NotFound(string message = "Not found!") => new(message);

    protected static SuccessServiceResult Success() => new();
    protected static SuccessServiceResult<T> Success<T>(T value) => new(value);

    protected static UnauthorizedServiceResult Unauthorized() => new();

    protected static ValidationErrorServiceResult ValidationError(IEnumerable<KeyValuePair<string, string>> errors) => new(errors);
    protected static ValidationErrorServiceResult ValidationError(string key, string value) => new(key, value);
    protected static ValidationErrorServiceResult ValidationError(ValidationResult result) => new FluentInvalidServiceResult(result);
}
