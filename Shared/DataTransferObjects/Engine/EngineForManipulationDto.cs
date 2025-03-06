using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Engine;

public record EngineForManipulationDto
{
    [Required(ErrorMessage = "Engine name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the name is 60 characters.")]
    public string? Name { get; init; }
    public string? Description { get; init; }

    [Url(ErrorMessage = "Website URL must be a valid URL.")]
    public string? WebsiteUrl { get; init; }
}