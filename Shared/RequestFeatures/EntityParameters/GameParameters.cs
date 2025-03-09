namespace Shared.RequestFeatures.EntityParameters;

public class GameParameters : RequestParameters
{
    public IEnumerable<int> FranchiseIds { get; set; } = new List<int>();
    public IEnumerable<int> EngineIds { get; set; } = new List<int>();
    public IEnumerable<int> PublisherIds { get; set; } = new List<int>();

    // Children
        // GameDevelopers
    public IEnumerable<int> DeveloperIds { get; set; } = new List<int>();
        // GameGenres
    public IEnumerable<int> GenreIds { get; set; } = new List<int>();
        // GameReleases
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public string? SearchTerm { get; set; }
}