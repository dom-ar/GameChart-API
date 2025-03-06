using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record GameGenresDto
{
    public int GameId { get; init; }
    public int GenreId { get; init; }
};