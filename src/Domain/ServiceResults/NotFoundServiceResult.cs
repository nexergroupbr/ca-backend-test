namespace Domain.ServiceResults;

public record NotFoundServiceResult(string Message)
    : NotSuccessServiceResult;
