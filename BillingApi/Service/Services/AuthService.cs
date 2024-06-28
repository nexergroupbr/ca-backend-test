using BillingApi.Service.Interfaces;
using BillingApi.ViewModels;

namespace BillingApi.Service.Services
{
    public class AuthService(IJwtHandler jwtHandler,
                             IConfiguration configuration) : IAuthService
    {
        #region Fields

        private readonly IJwtHandler _jwtHandler = jwtHandler;
        private readonly IConfiguration _configuration = configuration;

        #endregion

        public AuthViewModel Post(UsuarioViewModel payload)
        {
            var usuario = _configuration.GetValue<string>("Authentication:usuario");
            var senha = _configuration.GetValue<string>("Authentication:senha");

            var retorno = new AuthViewModel();
            if (usuario is not null && senha is not null && 
                usuario.Equals(payload.Login, StringComparison.OrdinalIgnoreCase) &&
                senha.Equals(payload.Senha, StringComparison.OrdinalIgnoreCase))
            {
                retorno.Token = _jwtHandler.GenerateToken(payload.Login);
                return retorno;
            }

            return retorno;
        }
    }
}
