using BillingApi.ViewModels;

namespace BillingApi.Service.Interfaces
{
    public interface IAuthService
    {
        AuthViewModel Post(UsuarioViewModel payload);
    }
}
