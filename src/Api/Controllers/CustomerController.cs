using DTO = Domain.Dtos.CustomerDTO;
using Service = Domain.Abstractions.Services.ICustomerService;
using ViewModel = Domain.Dtos.CustomerViewModel;

namespace Api.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CustomerController(IMapper mapper)
    : ServiceResultController
{
    private const string RouteNameById = "CustomerById";

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
                                                      [FromBody] DTO customer,
                                                      CancellationToken cancellationToken)
    {
        if (customer is null)
        {
            return BadRequest("Customer is null");
        }

        var result = await service.CreateAsync(customer, cancellationToken);

        if (result.IsSuccess)
        {
            return CreatedAtRoute<ViewModel>(mapper, RouteNameById, result.Value!);
        }

        return HandleServiceResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromServices] Service service,
                                            Guid id,
                                            [FromBody] DTO customer,
                                            CancellationToken cancellationToken)
    {
        if (customer is null)
        {
            return BadRequest("Customer is null");
        }

        var result = await service.UpdateAsync(id, customer, cancellationToken);
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