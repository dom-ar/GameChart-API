namespace Contracts
{
    public interface IRepositoryManager
    {
        IDeveloperRepository Developer { get; }
        IDlcReleaseRepository DlcRelease { get; }
        IDlcRepository Dlc { get; }
        IEngineRepository Engine { get; }
        IFranchiseRepository Franchise { get; }
        IGameReleaseRepository GameRelease { get; }
        IGameRepository Game { get; }
        IGenreRepository Genre { get; }
        IPlatformRepository Platform { get; }
        IPublisherRepository Publisher { get; }
        IStatusRepository Status { get; }
        IGameDevelopersRepository GameDevelopers { get; }
        IGameGenresRepository GameGenres { get; }
        IDlcDevelopersRepository DlcDevelopers { get; }
        IDlcGenresRepository DlcGenres { get; }

        Task SaveAsync();
    }
}
