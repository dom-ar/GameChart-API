using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Genre;

public record GenreForManipulationDto
{
    [Required(ErrorMessage = "Genre Name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Description is a required field.")]
    public string? Description { get; init; }
}