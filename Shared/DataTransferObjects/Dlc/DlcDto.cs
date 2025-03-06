using Shared.DataTransferObjects.Developer;
using Shared.DataTransferObjects.Genre;
using Shared.DataTransferObjects.Publisher;

namespace Shared.DataTransferObjects.Dlc;

// GET
public record DlcDto
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public int? ReleasedOnYear { get; init; }
    public int GameId { get; init; }
    public PublisherDto? Publisher { get; init; }
    public IEnumerable<BasicDeveloperDto>? Developers { get; init; }
    public IEnumerable<BasicGenreDto>? Genres { get; init; }
};