using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.GameGenres;

public record GameGenresForManipulationDto
{
    [Required(ErrorMessage = "GenreId is a required field.")]
    [Range(1, int.MaxValue, ErrorMessage = "GenreId is a required field and must be greater than zero.")]
    public int GenreId { get; init; }
}