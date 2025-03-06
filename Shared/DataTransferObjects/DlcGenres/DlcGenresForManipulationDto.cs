using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.DlcGenres;

public record DlcGenresForManipulationDto
{
    [Required(ErrorMessage = "GameId is a required field.")]
    [Range(1, int.MaxValue, ErrorMessage = "GenreId is a required field and must be greater than zero.")]
    public int GenreId { get; init; }
}