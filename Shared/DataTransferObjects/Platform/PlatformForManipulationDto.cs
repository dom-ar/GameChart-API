using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Platform;

public record PlatformForManipulationDto
{
    [Required(ErrorMessage = "Platform Name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; init; }
    public string? Description { get; init; }
}