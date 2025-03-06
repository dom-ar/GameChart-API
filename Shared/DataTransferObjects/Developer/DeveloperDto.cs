namespace Shared.DataTransferObjects.Developer;

public record DeveloperDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Location { get; init; }
    public string? Description { get; init; }
    public Uri? WebsiteUrl { get; init; }

};