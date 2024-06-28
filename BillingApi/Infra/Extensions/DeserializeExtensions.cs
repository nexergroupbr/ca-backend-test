using System.Text.Json;

namespace BillingApi.Infra.Extensions
{
    public static class DeserializeExtensions
    {
        private static readonly JsonSerializerOptions defaultSerializerSettings = new()
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        public static T DeserializeObject<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, defaultSerializerSettings);
        }
    }
}
