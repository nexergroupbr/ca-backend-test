namespace Domain.Dtos;

public record Pagination()
{
    public int Offset { get; set; } = 0;
    public int? Limit { get; set; }
    public string? OrderBy { get; set; }
    public bool Asc { get; set; } = true;
}
