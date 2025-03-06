namespace Shared.DataTransferObjects.Publisher;

public record PublisherDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Country { get; init; }
};

