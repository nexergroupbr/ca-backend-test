using DTO = Domain.Dtos.ProductDTO;
using Service = Domain.Abstractions.Services.IProductService;
using ViewModel = Domain.Dtos.ProductViewModel;

namespace Api.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ProductController(IMapper mapper)
    : ServiceResultController
{
    private const string RouteNameById = "ProductById";

    [HttpGet]
    public async Task<ActionResult<List<ViewModel>>> GetAll([FromServices] Service service,
                                                            [FromQuery] Pagination pagination,
                                                            CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync<ViewModel>(pagination, cancellationToken);
        return HandleServiceResult(result);
    }

    [HttpGet("{id}", Name = RouteNameById)]
    public async Task<ActionResult<ViewModel>> GetById([FromServices] Service service,
                                                       Guid id,
                                                       CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync<ViewModel>(id, cancellationToken);
        return HandleServiceResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<ViewModel>> Create([FromServices] Service service,
                                                      [FromBody] DTO product,
                                                      CancellationToken cancellationToken)
    {
        if (product is null)
        {
            return BadRequest("Product is null");
        }

        var result = await service.CreateAsync(product, cancellationToken);

        if (result.IsSuccess)
        {
            return CreatedAtRoute<ViewModel>(mapper, RouteNameById, result.Value!);
        }

        return HandleServiceResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromServices] Service service,
                                            Guid id,
                                            [FromBody] DTO product,
                                            CancellationToken cancellationToken)
    {
        if (product is null)
        {
            return BadRequest("Product is null");
        }

        var result = await service.UpdateAsync(id, product, cancellationToken);
        return HandleServiceResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromServices] Service service,
                                            Guid id,
                                            CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken);
        return HandleServiceResult(result);
    }
}