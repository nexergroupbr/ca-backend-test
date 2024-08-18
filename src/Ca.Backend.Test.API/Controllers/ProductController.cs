using Ca.Backend.Test.API.Models.Response.Api;
using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;
using Ca.Backend.Test.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ca.Backend.Test.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductServices _productServices;

    public ProductController(IProductServices productServices)
    {
        _productServices = productServices;
    }

    /// <summary> Cria um novo produto </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     POST /api/product
    ///     {
    ///        "description": "Descrição do Produto A"
    ///     }
    ///     
    /// </remarks>
    /// <param name="request">Dados do produto</param>
    /// <returns>Retorna o produto criado</returns>
    /// <response code="200">OK - Produto criado com sucesso</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpPost]
    [ProducesResponseType(typeof(GenericHttpResponse<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequest request)
    {
        var response = await _productServices.CreateAsync(request);
        return Ok(new GenericHttpResponse<ProductResponse>
        {
            Data = response
        });
    }

    /// <summary> Obtém um produto pelo ID </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     GET /api/product/{id}
    ///     
    /// </remarks>
    /// <param name="id">ID do produto</param>
    /// <returns>Retorna o produto correspondente</returns>
    /// <response code="200">OK - Produto encontrado</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GenericHttpResponse<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProductByIdAsync(Guid id)
    {
        var response = await _productServices.GetByIdAsync(id);
        return Ok(new GenericHttpResponse<ProductResponse>
        {
            Data = response
        });
    }

    /// <summary> Obtém todos os produtos </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     GET /api/product
    ///     
    /// </remarks>
    /// <returns>Retorna uma lista de produtos</returns>
    /// <response code="200">OK - Produtos encontrados</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpGet]
    [ProducesResponseType(typeof(GenericHttpResponse<IEnumerable<ProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        var response = await _productServices.GetAllAsync();
        return Ok(new GenericHttpResponse<IEnumerable<ProductResponse>>
        {
            Data = response
        });
    }

    /// <summary> Atualiza um produto pelo ID </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     PUT /api/product/{id}
    ///     {
    ///        "description": "Descrição do Produto A"
    ///     }
    ///     
    /// </remarks>
    /// <param name="id">ID do produto</param>
    /// <param name="request">Dados do produto</param>
    /// <returns>Retorna o produto atualizado</returns>
    /// <response code="200">OK - Produto atualizado com sucesso</response>
    /// <response code="400">Bad Request - Requisição do Cliente é Inválida</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(GenericHttpResponse<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericHttpResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProductAsync(Guid id, [FromBody] ProductRequest request)
    {
        var response = await _productServices.UpdateAsync(id, request);
        return Ok(new GenericHttpResponse<ProductResponse>
        {
            Data = response
        });
    }

    /// <summary> Deleta um produto pelo ID </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     DELETE /api/product/{id}
    ///     
    /// </remarks>
    /// <param name="id">ID do produto</param>
    /// <response code="204">No Content - Produto deletado com sucesso</response>
    /// <response code="404">Not Found - Produto não encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        await _productServices.DeleteByIdAsync(id);
        return NoContent();
    }
}
