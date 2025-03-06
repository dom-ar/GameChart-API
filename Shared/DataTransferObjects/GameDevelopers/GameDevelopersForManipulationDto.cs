using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.GameDevelopers;

public record GameDevelopersForManipulationDto
{
    [Required(ErrorMessage = "DeveloperId is a required field.")]
    [Range(1, int.MaxValue, ErrorMessage = "DeveloperId is a required field and must be greater than zero.")]
    public int DeveloperId { get; init; }
}