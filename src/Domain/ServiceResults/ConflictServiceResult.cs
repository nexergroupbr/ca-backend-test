namespace Domain.ServiceResults;

public record ConflictServiceResult(string Message)
    : NotSuccessServiceResult;
