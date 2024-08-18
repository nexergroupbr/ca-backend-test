using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Ca.Backend.Test.API.Models.Response.Api;
public class GenericHttpResponse<T>
{
    [JsonPropertyName("trace_id")]
    public string? TraceId { get; set; }

    [JsonIgnore]
    public int StatusCode { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("errors")]
    public IEnumerable<string> Errors { get; set; }

    public GenericHttpResponse()
    {
        TraceId = Activity.Current?.Id ?? Guid.NewGuid().ToString();
        Errors = Enumerable.Empty<string>();
    }
}

public class GenericHttpResponse : GenericHttpResponse<object>
{
    public GenericHttpResponse() : base() { }
}

