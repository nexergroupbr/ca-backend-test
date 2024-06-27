namespace ExternalApiClient.Exceptions;

public class ApiMalFuncException
    : Exception
{
    internal ApiMalFuncException(string? message, Exception? innerException)
        : base(message, innerException)
    {

    }
}
