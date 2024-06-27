namespace Api.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BillingImportController
    : ServiceResultController
{
    [HttpPost]
    public async Task<ActionResult> Import([FromServices] IBillingImportService service, CancellationToken cancellationToken)
    {
        var result = await service.ImportAsync(cancellationToken);
        return HandleServiceResult(result);
    }
}
