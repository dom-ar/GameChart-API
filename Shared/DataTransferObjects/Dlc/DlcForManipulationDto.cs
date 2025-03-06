using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Dlc;

public record DlcForManipulationDto
{
    [Required(ErrorMessage = "Dlc Title is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
    public string? Title { get; init; }
    public string? Description { get; init; }
    public int? ReleasedOnYear { get; init; }
    [Required(ErrorMessage = "PublisherId is a required field.")]
    [Range(1, int.MaxValue, ErrorMessage = "PublisherId is a required field and must be greater than zero.")]
    public int PublisherId { get; init; }
}