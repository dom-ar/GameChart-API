using System.ComponentModel.DataAnnotations;
using Shared.DataTransferObjects.Developer;

namespace Shared.DataTransferObjects.GameDevelopers;

public record GameDevelopersDto
{
    public int GameId { get; init; }
    //public int DeveloperId { get; init; }
    public BasicDeveloperDto? Developer { get; init; }
};

