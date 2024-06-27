using Service = Domain.Abstractions.Services.IBillingService;
using ViewModel = Domain.Dtos.BillingViewModel;

namespace Api.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BillingController
    : ServiceResultController
{
    [HttpGet]
    public async Task<ActionResult<List<ViewModel>>> GetAll([FromServices] Service service,
                                                            [FromQuery] Pagination pagination,
                                                            CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync<ViewModel>(pagination, cancellationToken);
        return HandleServiceResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ViewModel>> GetById([FromServices] Service service,
                                                       Guid id,
                                                       CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync<ViewModel>(id, cancellationToken);
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