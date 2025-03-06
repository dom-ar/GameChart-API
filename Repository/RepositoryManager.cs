using Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IDeveloperRepository> _developerRepository;
        private readonly Lazy<IDlcReleaseRepository> _dlcReleaseRepository;
        private readonly Lazy<IDlcRepository> _dlcRepository;
        private readonly Lazy<IEngineRepository> _engineRepository;
        private readonly Lazy<IFranchiseRepository> _franchiseRepository;
        private readonly Lazy<IGameReleaseRepository> _gameReleaseRepository;
        private readonly Lazy<IGenreRepository> _genreRepository;
        private readonly Lazy<IPublisherRepository> _publisherRepository;
        private readonly Lazy<IStatusRepository> _statusRepository;
        private readonly Lazy<IGameRepository> _gameRepository;
        private readonly Lazy<IPlatformRepository> _platformRepository;
        private readonly Lazy<IGameDevelopersRepository> _gameDevelopersRepository;
        private readonly Lazy<IGameGenresRepository> _gameGenresRepository;
        private readonly Lazy<IDlcDevelopersRepository> _dlcDevelopersRepository;
        private readonly Lazy<IDlcGenresRepository> _dlcGenresRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _developerRepository = new Lazy<IDeveloperRepository>(() => new DeveloperRepository(repositoryContext));
            _dlcReleaseRepository = new Lazy<IDlcReleaseRepository>(() => new DlcReleaseRepository(repositoryContext));
            _dlcRepository = new Lazy<IDlcRepository>(() => new DlcRepository(repositoryContext));
            _engineRepository = new Lazy<IEngineRepository>(() => new EngineRepository(repositoryContext));
            _franchiseRepository = new Lazy<IFranchiseRepository>(() => new FranchiseRepository(repositoryContext));
            _gameReleaseRepository = new Lazy<IGameReleaseRepository>(() => new GameReleaseRepository(repositoryContext));
            _genreRepository = new Lazy<IGenreRepository>(() => new GenreRepository(repositoryContext));
            _publisherRepository = new Lazy<IPublisherRepository>(() => new PublisherRepository(repositoryContext));
            _statusRepository = new Lazy<IStatusRepository>(() => new StatusRepository(repositoryContext));
            _gameRepository = new Lazy<IGameRepository>(() => new GameRepository(repositoryContext));
            _platformRepository = new Lazy<IPlatformRepository>(() => new PlatformRepository(repositoryContext));
            _gameDevelopersRepository = new Lazy<IGameDevelopersRepository>(() => new GameDevelopersRepository(repositoryContext));
            _gameGenresRepository = new Lazy<IGameGenresRepository>(() => new GameGenresRepository(repositoryContext));
            _dlcDevelopersRepository = new Lazy<IDlcDevelopersRepository>(() => new DlcDevelopersRepository(repositoryContext));
            _dlcGenresRepository = new Lazy<IDlcGenresRepository>(() => new DlcGenresRepository(repositoryContext));
        }

        public IDeveloperRepository Developer => _developerRepository.Value;
        public IDlcReleaseRepository DlcRelease => _dlcReleaseRepository.Value;
        public IDlcRepository Dlc => _dlcRepository.Value;
        public IEngineRepository Engine => _engineRepository.Value;
        public IFranchiseRepository Franchise => _franchiseRepository.Value;
        public IGameReleaseRepository GameRelease => _gameReleaseRepository.Value;
        public IGenreRepository Genre => _genreRepository.Value;
        public IPublisherRepository Publisher => _publisherRepository.Value;
        public IStatusRepository Status => _statusRepository.Value;
        public IGameRepository Game => _gameRepository.Value;
        public IPlatformRepository Platform => _platformRepository.Value;
        public IGameDevelopersRepository GameDevelopers => _gameDevelopersRepository.Value;
        public IGameGenresRepository GameGenres => _gameGenresRepository.Value;
        public IDlcDevelopersRepository DlcDevelopers => _dlcDevelopersRepository.Value;
        public IDlcGenresRepository DlcGenres => _dlcGenresRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
