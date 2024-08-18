using Ca.Backend.Test.API.Models.Response.Api;
using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;
using Ca.Backend.Test.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ca.Backend.Test.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerServices _customerServices;

    public CustomerController(ICustomerServices customerServices)
    {
        _customerServices = customerServices;
    }

    /// <summary> Cria um novo cliente </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     POST /api/customer
    ///     {
    ///        "name": "John Doe",
    ///        "email": "john.doe@example.com",
    ///        "address": "123 Main St"
    ///     }
    ///     
    /// </remarks>
    /// <param name="request">Dados do cliente</param>
    /// <returns>Retorna o cliente criado</returns>
    /// <response code="200">OK - Cliente criado com sucesso</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpPost]
    [ProducesResponseType(typeof(GenericHttpResponse<CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerRequest request)
    {
        var response = await _customerServices.CreateAsync(request);
        return Ok(new GenericHttpResponse<CustomerResponse>
        {
            Data = response
        });
    }

    /// <summary> Obtém um cliente pelo ID </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     GET /api/customer/{id}
    ///     
    /// </remarks>
    /// <param name="id">ID do cliente</param>
    /// <returns>Retorna o cliente correspondente</returns>
    /// <response code="200">OK - Cliente encontrado</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GenericHttpResponse<CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCustomerByIdAsync(Guid id)
    {
        var response = await _customerServices.GetByIdAsync(id);
        return Ok(new GenericHttpResponse<CustomerResponse>
        {
            Data = response
        });
    }

    /// <summary> Obtém todos os clientes </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     GET /api/customer
    ///     
    /// </remarks>
    /// <returns>Retorna uma lista de clientes</returns>
    /// <response code="200">OK - Clientes encontrados</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpGet]
    [ProducesResponseType(typeof(GenericHttpResponse<IEnumerable<CustomerResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllCustomersAsync()
    {
        var response = await _customerServices.GetAllAsync();
        return Ok(new GenericHttpResponse<IEnumerable<CustomerResponse>>
        {
            Data = response
        });
    }

    /// <summary> Atualiza um cliente pelo ID </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     PUT /api/customer/{id}
    ///     {
    ///        "name": "John Doe",
    ///        "email": "john.doe@example.com",
    ///        "address": "123 Main St"
    ///     }
    ///     
    /// </remarks>
    /// <param name="id">ID do cliente</param>
    /// <param name="request">Dados do cliente</param>
    /// <returns>Retorna o cliente atualizado</returns>
    /// <response code="200">OK - Cliente atualizado com sucesso</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(GenericHttpResponse<CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomerAsync(Guid id, [FromBody] CustomerRequest request)
    {
        var response = await _customerServices.UpdateAsync(id, request);
        return Ok(new GenericHttpResponse<CustomerResponse>
        {
            Data = response
        });
    }

    /// <summary> Deleta um cliente pelo ID </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     DELETE /api/customer/{id}
    ///     
    /// </remarks>
    /// <param name="id">ID do cliente</param>
    /// <response code="204">No Content - Cliente deletado com sucesso</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCustomerAsync(Guid id)
    {
        await _customerServices.DeleteByIdAsync(id);
        return NoContent();
    }
}
