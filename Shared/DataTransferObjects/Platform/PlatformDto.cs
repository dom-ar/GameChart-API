namespace Shared.DataTransferObjects.Platform;

public record PlatformDto
{
    public int Id { get; init; } 
    public string? Name { get; init; }
    public string? Description { get; init; }
};

