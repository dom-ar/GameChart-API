using Shared.DataTransferObjects.Game;
using Shared.DataTransferObjects.Platform;
using Shared.DataTransferObjects.Status;

namespace Shared.DataTransferObjects.GameRelease;

public record GameReleaseDto
{
    public int Id { get; init; }
    public string? TimeFrame { get; init; }
    public DateTime? ReleaseDate { get; init; }
    public Uri? StoreUrl { get; init; }
    public BasicGameDto? Game { get; init; }
    public BasicPlatformDto? Platform { get; init; }
    public StatusDto? Status { get; init; }
};