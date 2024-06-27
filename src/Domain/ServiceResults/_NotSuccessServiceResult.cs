namespace Domain.ServiceResults;

public abstract record NotSuccessServiceResult
    : ServiceResult
{
    public override bool IsSuccess { get; init; } = false;
}
