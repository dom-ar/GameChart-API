using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Developer;

public record DeveloperForManipulationDto
{
    [Required(ErrorMessage = "Developer Name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Developer Name is 60 characters")]
    public string? Name { get; init; }

    public string? Location { get; init; }
    public string? Description { get; init; }

    [Url(ErrorMessage = "Website URL must be a valid URL.")]
    public string? WebsiteUrl { get; init; }
}