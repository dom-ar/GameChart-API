using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.DlcDevelopers;

public record DlcDevelopersForManipulationDto
{
    [Required(ErrorMessage = "DeveloperId is a required field.")]
    [Range(1, int.MaxValue, ErrorMessage = "DeveloperId must be greater than zero.")]
    public int DeveloperId { get; init; }
}