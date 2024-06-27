namespace Domain.ServiceResults;

public record InternalErrorServiceResult(string Message)
    : NotSuccessServiceResult;
