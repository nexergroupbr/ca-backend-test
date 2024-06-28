namespace BillingApi.Infra.Exceptions
{
    public class ServiceUnavailableException(string message) : Exception(message)
    {
    }
}
