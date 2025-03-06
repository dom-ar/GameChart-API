using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Status;

public record StatusForManipulationDto
{
    [Required(ErrorMessage = "Status Name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; init; }
}