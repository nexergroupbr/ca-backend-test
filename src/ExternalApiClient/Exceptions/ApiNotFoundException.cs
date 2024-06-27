namespace ExternalApiClient.Exceptions;

public class ApiNotFoundException
    : Exception
{
    internal ApiNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
        
    }
}
