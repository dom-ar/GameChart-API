namespace Shared.DataTransferObjects.Developer;

public record BasicDeveloperDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
};