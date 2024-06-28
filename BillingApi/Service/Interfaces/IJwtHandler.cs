namespace BillingApi.Service.Interfaces
{
    public interface IJwtHandler
    {
        string GenerateToken(string login, int expiryMinutes = 0);
    }
}
