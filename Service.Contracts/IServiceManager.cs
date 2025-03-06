namespace Service.Contracts
{
    public interface IServiceManager
    {
        IDeveloperService DeveloperService { get; }
        IDlcReleaseService DlcReleaseService { get; }
        IDlcService DlcService { get; }
        IEngineService EngineService { get; }
        IFranchiseService FranchiseService { get; }
        IGameReleaseService GameReleaseService { get; }
        IGameService GameService { get; }
        IGenreService GenreService { get; }
        IPlatformService PlatformService { get; }
        IPublisherService PublisherService { get; }
        IStatusService StatusService { get; }
        IGameDevelopersService GameDevelopersService { get; }
        IGameGenresService GameGenresService { get; }
        IDlcGenresService DlcGenresService { get; }
        IDlcDevelopersService DlcDevelopersService { get; }
        IReleaseService ReleaseService { get; }
    }
}
