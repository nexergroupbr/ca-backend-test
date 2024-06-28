namespace BillingApi.Infra.Exceptions
{
    public class ApiException(string message) : Exception(message)
    {
    }
}
