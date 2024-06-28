using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BillingApi.Infra.Exceptions
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ServiceUnavailableException)
            {
                context.Result = new ObjectResult("O serviço está indisponível.")
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable
                };
            }
            else if (context.Exception is ApiException)
            {
                context.Result = new ObjectResult("Erro ao chamar a API.")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.ExceptionHandled = true;
        }
    }
}
