#nullable disable

namespace ExternalApiClient.Models;

public record Customer
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }
}
