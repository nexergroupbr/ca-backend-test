using BillingApi.Service.Interfaces;
using BillingApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BillingApi.Controllers
{
    [ApiController]
    [Route("api/Authentication")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Autentica um usuário no sistema
        /// </summary>
        [Route("")]
        [HttpPost]
        [ProducesDefaultResponseType(typeof(AuthViewModel))]
        public IActionResult Login(UsuarioViewModel payload)
        {
            try
            {
                var response = _authService.Post(payload);

                if (!string.IsNullOrEmpty(response.Token))
                    return Ok(response);
                else
                    return Unauthorized("Usuário ou senha inválidos.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " " + e.InnerException?.Message);
            }
        }
    }
}
