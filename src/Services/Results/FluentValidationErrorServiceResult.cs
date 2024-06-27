namespace Services.Results;

internal record FluentInvalidServiceResult
    : ValidationErrorServiceResult
{
    public FluentInvalidServiceResult(ValidationResult validationResult)
        : base([])
        => Errors = validationResult.Errors.ConvertAll(q => KeyValuePair.Create(q.PropertyName, q.ErrorMessage));
}