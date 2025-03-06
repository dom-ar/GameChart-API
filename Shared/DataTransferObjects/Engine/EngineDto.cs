namespace Shared.DataTransferObjects.Engine;

public record EngineDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    Uri? WebsiteUrl { get; init; }
};

public record BasicEngineDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
}
