using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.DlcRelease;

public record DlcReleaseForManipulationDto
{
    public string? TimeFrame { get; init; }
    public DateTime? ReleaseDate { get; init; }

    [Url(ErrorMessage = "Website URL must be a valid URL.")]
    public string? StoreUrl { get; init; }

    [Required(ErrorMessage = "PlatformId is a required field to link a release.")]
    [Range(1, int.MaxValue, ErrorMessage = "PlatformId is a required field to link a release and must be greater than zero.")]
    public int PlatformId { get; init; }

    [Required(ErrorMessage = "StatusId is a required field to link a release.")]
    [Range(1, int.MaxValue, ErrorMessage = "StatusId is a required field to link a release and must be greater than zero.")]
    public int StatusId { get; init; }
}