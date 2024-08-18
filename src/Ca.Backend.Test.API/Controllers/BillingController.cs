using Ca.Backend.Test.API.Models.Response.Api;
using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;
using Ca.Backend.Test.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ca.Backend.Test.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillingController : ControllerBase
{
    private readonly IBillingServices _billingServices;

    public BillingController(IBillingServices billingServices)
    {
        _billingServices = billingServices;
    }

    /// <summary> Importa faturas de uma API externa </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     POST /api/billing/importFromExternalApi
    ///     
    /// </remarks>
    /// <response code="204">No Content - Importação bem-sucedida</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpPost("importFromExternalApi")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ImportBillingAsync()
    {
        await _billingServices.ImportBillingFromExternalApiAsync();
        return NoContent();
    }

    /// <summary> Cria uma nova fatura </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     POST /api/billing
    ///     {
    ///        "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///        "invoiceNumber": "INV-001",
    ///        "date": "2024-07-02T03:43:17.495Z",
    ///        "dueDate": "2024-07-02T03:43:17.495Z",
    ///        "totalAmount": 150.00,
    ///        "currency": "USD",
    ///        "lines": [
    ///            {
    ///                "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///                "quantity": 2,
    ///                "unitPrice": 75.0,
    ///                "subtotal": 150.0
    ///            }
    ///        ]
    ///     }
    ///     
    /// </remarks>
    /// <param name="request">Dados da fatura</param>
    /// <returns>Retorna a fatura criada</returns>
    /// <response code="200">OK - Fatura criada com sucesso</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpPost]
    [ProducesResponseType(typeof(GenericHttpResponse<BillingResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBillingAsync([FromBody] BillingRequest request)
    {
        var response = await _billingServices.CreateAsync(request);
        return Ok(new GenericHttpResponse<BillingResponse>
        {
            Data = response
        });
    }

    /// <summary> Obtém uma fatura pelo ID </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     GET /api/billing/{id}
    ///     
    /// </remarks>
    /// <param name="id">ID da fatura</param>
    /// <returns>Retorna a fatura correspondente</returns>
    /// <response code="200">OK - Fatura encontrada</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GenericHttpResponse<BillingResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBillingByIdAsync(Guid id)
    {
        var response = await _billingServices.GetByIdAsync(id);
        return Ok(new GenericHttpResponse<BillingResponse>
        {
            Data = response
        });
    }

    /// <summary> Obtém todas as faturas </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     GET /api/billing
    ///     
    /// </remarks>
    /// <returns>Retorna uma lista de faturas</returns>
    /// <response code="200">OK - Faturas encontradas</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpGet]
    [ProducesResponseType(typeof(GenericHttpResponse<IEnumerable<BillingResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllBillingsAsync()
    {
        var response = await _billingServices.GetAllAsync();
        return Ok(new GenericHttpResponse<IEnumerable<BillingResponse>>
        {
            Data = response
        });
    }

    /// <summary> Atualiza uma fatura pelo ID </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     PUT /api/billing/{id}
    ///     {
    ///        "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///        "invoiceNumber": "INV-001",
    ///        "date": "2024-07-02T03:43:17.495Z",
    ///        "dueDate": "2024-07-02T03:43:17.495Z",
    ///        "totalAmount": 150.75,
    ///        "currency": "USD",
    ///        "lines": [
    ///            {
    ///                "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///                "quantity": 3,
    ///                "unitPrice": 50.25,
    ///                "subtotal": 150.75
    ///            }
    ///        ]
    ///     }
    ///     
    /// </remarks>
    /// <param name="id">ID da fatura</param>
    /// <param name="request">Dados da fatura</param>
    /// <returns>Retorna a fatura atualizada</returns>
    /// <response code="200">OK - Fatura atualizada com sucesso</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(GenericHttpResponse<BillingResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBillingAsync(Guid id, [FromBody] BillingRequest request)
    {
        var response = await _billingServices.UpdateAsync(id, request);
        return Ok(new GenericHttpResponse<BillingResponse>
        {
            Data = response
        });
    }

    /// <summary> Deleta uma fatura pelo ID </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     DELETE /api/billing/{id}
    ///     
    /// </remarks>
    /// <param name="id">ID da fatura</param>
    /// <response code="204">No Content - Fatura deletada com sucesso</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteBillingAsync(Guid id)
    {
        await _billingServices.DeleteByIdAsync(id);
        return NoContent();
    }
}
