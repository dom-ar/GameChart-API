using System.ComponentModel.DataAnnotations;
using Shared.DataTransferObjects.Dlc;
using Shared.DataTransferObjects.Platform;
using Shared.DataTransferObjects.Status;

namespace Shared.DataTransferObjects.DlcRelease;

public record DlcReleaseDto
{
    public int Id { get; init; }
    public string? TimeFrame { get; init; }
    public DateTime? ReleaseDate { get; init; }
    public DlcDto? Dlc { get; init; }
    public BasicPlatformDto? Platform { get; init; }
    public StatusDto? Status { get; init; }

};