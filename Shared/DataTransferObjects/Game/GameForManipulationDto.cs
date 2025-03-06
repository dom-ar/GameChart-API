using System.ComponentModel.DataAnnotations;
using Shared.DataTransferObjects.GameDevelopers;
using Shared.DataTransferObjects.GameGenres;

namespace Shared.DataTransferObjects.Game;

public abstract record GameForManipulationDto
{
    [Required(ErrorMessage = "Game Title is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
    public string? Title { get; init; }
    public string? Description { get; init; }
    public int? ReleasedOnYear { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "FranchiseId must be greater than zero.")]
    public int? FranchiseId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "EngineId must be greater than zero.")]
    public int? EngineId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "PublisherId must be greater than zero.")]
    public int? PublisherId { get; init; }

    public IEnumerable<GameDevelopersForCreationDto>? Developers { get; init; }
    public IEnumerable<GameGenresForCreationDto>? Genres { get; init; }

};
