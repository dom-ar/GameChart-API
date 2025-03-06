namespace Shared.DataTransferObjects.Platform;

public record BasicPlatformDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
};
