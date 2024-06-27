namespace Domain.ServiceResults;

public record InvalidServiceResult(string Message)
    : NotSuccessServiceResult;
