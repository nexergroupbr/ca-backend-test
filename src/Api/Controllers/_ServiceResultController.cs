namespace Api.Controllers;

public abstract class ServiceResultController
    : ControllerBase
{
    protected ActionResult HandleServiceResult(IServiceResult result)
    {
        return (result.Result ?? result) switch
        {
            IValuedSuccessServiceResult r => Ok(r.Value),
            SuccessServiceResult _ => NoContent(),
            ForbiddenServiceResult _ => Forbid(),
            InvalidServiceResult r => BadRequest(r.Message),
            ValidationErrorServiceResult r => InvalidResult(r),
            ConflictServiceResult r => Conflict(r.Message),
            NotFoundServiceResult r => NotFound(r.Message),
            UnauthorizedServiceResult _ => Unauthorized(),
            InternalErrorServiceResult r => StatusCode(500, r.Message),
            _ => throw new Exception("Unknown type of ServiceResult")
        };
    }

    private ActionResult InvalidResult(ValidationErrorServiceResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }

        return ValidationProblem();
    }

    protected CreatedAtRouteResult CreatedAtRoute<T>(IMapper mapper, string? routeName, BaseEntity value)
    {
        var mapedValue = Map<T>(mapper, value);

        return CreatedAtRoute(routeName, new { id = value.Id }, mapedValue);
    }

    private static object? Map<T>(IMapper? mapper, object value)
    {
        if (mapper is not null && typeof(T) != typeof(object))
        {
            return mapper.Map<T>(value);
        }

        return value;
    }
}
