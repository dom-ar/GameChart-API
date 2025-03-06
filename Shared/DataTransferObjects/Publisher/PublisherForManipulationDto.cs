using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Publisher;

public record PublisherForManipulationDto
{
    [Required(ErrorMessage = "Publisher Name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; init; }
    public string? Country { get; init; }
}