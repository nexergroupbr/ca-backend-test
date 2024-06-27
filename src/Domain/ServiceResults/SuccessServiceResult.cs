namespace Domain.ServiceResults;


public interface IValuedSuccessServiceResult
{
    object? Value { get; }
}

public record SuccessServiceResult
    : ServiceResult
{
    public override bool IsSuccess { get; init; } = true;
}

public record SuccessServiceResult<T>(T Value)
    : ServiceResult<T>(Value),
    IValuedSuccessServiceResult
{
    public override bool IsSuccess { get; init; } = true;

    object? IValuedSuccessServiceResult.Value => Value;
}
