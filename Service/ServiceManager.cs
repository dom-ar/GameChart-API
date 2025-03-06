using Contracts;
using Service.Contracts;
using AutoMapper;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IDeveloperService> _developerService;
        private readonly Lazy<DlcReleaseService> _dlcReleaseService;
        private readonly Lazy<DlcService> _dlcService;
        private readonly Lazy<EngineService> _engineService;
        private readonly Lazy<FranchiseService> _franchiseService;
        private readonly Lazy<GameReleaseService> _gameReleaseService;
        private readonly Lazy<GameService> _gameService;
        private readonly Lazy<GenreService> _genreService;
        private readonly Lazy<PlatformService> _platformService;
        private readonly Lazy<PublisherService> _publisherService;
        private readonly Lazy<StatusService> _statusService;
        private readonly Lazy<GameDevelopersService> _gameDevelopersService;
        private readonly Lazy<GameGenresService> _gameGenresService;
        private readonly Lazy<DlcGenresService> _dlcGenresService;
        private readonly Lazy<DlcDevelopersService> _dlcDevelopersService;
        private readonly Lazy<ReleaseService> _releaseService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _developerService = new Lazy<IDeveloperService>(() => new DeveloperService(repositoryManager, logger, mapper));
            _dlcReleaseService = new Lazy<DlcReleaseService>(() => new DlcReleaseService(repositoryManager, logger, mapper));
            _dlcService = new Lazy<DlcService>(() => new DlcService(repositoryManager, logger, mapper));
            _engineService = new Lazy<EngineService>(() => new EngineService(repositoryManager, logger, mapper));
            _franchiseService = new Lazy<FranchiseService>(() => new FranchiseService(repositoryManager, logger, mapper));
            _gameReleaseService = new Lazy<GameReleaseService>(() => new GameReleaseService(repositoryManager, logger, mapper));
            _gameService = new Lazy<GameService>(() => new GameService(repositoryManager, logger, mapper));
            _genreService = new Lazy<GenreService>(() => new GenreService(repositoryManager, logger, mapper));
            _platformService = new Lazy<PlatformService>(() => new PlatformService(repositoryManager, logger, mapper));
            _publisherService = new Lazy<PublisherService>(() => new PublisherService(repositoryManager, logger, mapper));
            _statusService = new Lazy<StatusService>(() => new StatusService(repositoryManager, logger, mapper));
            _gameDevelopersService = new Lazy<GameDevelopersService>(() => new GameDevelopersService(repositoryManager, logger, mapper));
            _gameGenresService = new Lazy<GameGenresService>(() => new GameGenresService(repositoryManager, logger, mapper));
            _dlcGenresService = new Lazy<DlcGenresService>(() => new DlcGenresService(repositoryManager, logger, mapper));
            _dlcDevelopersService = new Lazy<DlcDevelopersService>(() => new DlcDevelopersService(repositoryManager, logger, mapper));
            _releaseService = new Lazy<ReleaseService>(() => new ReleaseService(repositoryManager, logger, mapper));
        }

        public IDeveloperService DeveloperService => _developerService.Value;
        public IDlcReleaseService DlcReleaseService => _dlcReleaseService.Value;
        public IDlcService DlcService => _dlcService.Value;
        public IEngineService EngineService => _engineService.Value;
        public IFranchiseService FranchiseService => _franchiseService.Value;
        public IGameReleaseService GameReleaseService => _gameReleaseService.Value;
        public IGameService GameService => _gameService.Value;
        public IGenreService GenreService => _genreService.Value;
        public IPlatformService PlatformService => _platformService.Value;
        public IPublisherService PublisherService => _publisherService.Value;
        public IStatusService StatusService => _statusService.Value;
        public IGameDevelopersService GameDevelopersService => _gameDevelopersService.Value;
        public IGameGenresService GameGenresService => _gameGenresService.Value;
        public IDlcGenresService DlcGenresService => _dlcGenresService.Value;
        public IDlcDevelopersService DlcDevelopersService => _dlcDevelopersService.Value;
        public IReleaseService ReleaseService => _releaseService.Value;
    }
}
