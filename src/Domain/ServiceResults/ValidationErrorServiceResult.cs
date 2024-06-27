namespace Domain.ServiceResults;

public record ValidationErrorServiceResult(IEnumerable<KeyValuePair<string, string>> Errors)
    : NotSuccessServiceResult
{
    public ValidationErrorServiceResult(string key, string value)
        : this([new KeyValuePair<string, string>(key, value)])
    {

    }
}
