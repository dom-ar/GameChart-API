using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Entities.Models.Joint_Models;
using Service.Contracts;
using Shared.DataTransferObjects.Game;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service
{
    internal sealed class GameService : IGameService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public GameService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET

        // Get all games (basic information)
        public async Task<(IEnumerable<BasicGameDto> games, MetaData metaData)> GetAllGamesAsync(GameParameters gameParameters, bool trackChanges)
        {
            // Check if filters are valid
            if (gameParameters.FranchiseIds.Any())
            {
                var franchises =
                    await _repository.Franchise.GetFranchisesByIdsAsync(gameParameters.FranchiseIds, false);
                var invalidFranchises = gameParameters.FranchiseIds.Except(franchises.Select(f => f.Id));

                var invalidFranchisesList = invalidFranchises.ToList();
                if (invalidFranchisesList.Any())
                    throw new FranchiseNotFoundException(invalidFranchisesList);
            }

            if (gameParameters.EngineIds.Any())
            {
                var engines = await _repository.Engine.GetEnginesByIdsAsync(gameParameters.EngineIds, false);
                var invalidEngines = gameParameters.EngineIds.Except(engines.Select(e => e.Id));

                var invalidEnginesList = invalidEngines.ToList();
                if (invalidEnginesList.Any())
                    throw new EngineNotFoundException(invalidEnginesList);
            }

            if (gameParameters.PublisherIds.Any())
            {
                var publishers =
                    await _repository.Publisher.GetPublishersByIdsAsync(gameParameters.PublisherIds, false);
                var invalidPublishers = gameParameters.PublisherIds.Except(publishers.Select(p => p.Id));

                var invalidPublishersList = invalidPublishers.ToList();
                if (invalidPublishersList.Any())
                    throw new PublisherNotFoundException(invalidPublishersList);
            }

            if (gameParameters.DeveloperIds.Any())
            {
                var developers =
                    await _repository.Developer.GetDevelopersByIdsAsync(gameParameters.DeveloperIds, false);
                var invalidDevelopers = gameParameters.DeveloperIds.Except(developers.Select(d => d.Id));

                var invalidDevelopersList = invalidDevelopers.ToList();
                if (invalidDevelopersList.Any())
                    throw new DeveloperNotFoundException(invalidDevelopersList);
            }

            if (gameParameters.GenreIds.Any())
            {
                var genres = await _repository.Genre.GetGenresByIdsAsync(gameParameters.GenreIds, false);
                var invalidGenres = gameParameters.GenreIds.Except(genres.Select(g => g.Id));
                var invalidGenresList = invalidGenres.ToList();
                if (invalidGenresList.Any())
                    throw new GenreNotFoundException(invalidGenresList);
            }

            //if (!gameParameters.MinYear.HasValue && !gameParameters.MaxYear.HasValue)
            var minYear = gameParameters.MinYear;
            var maxYear = gameParameters.MaxYear;

            if (minYear.HasValue && minYear < 0)
                        throw new InvalidYearRangeException(minYear.Value, "minYear");

            if (maxYear.HasValue && maxYear < 0)
                throw new InvalidYearRangeException(maxYear.Value, "maxYear");

            if (minYear.HasValue && minYear > maxYear)
                throw new InvalidYearRangeException(minYear.Value, maxYear.Value);

            // Get games
            var gamesWithMetaData = await _repository.Game.GetAllGamesAsync(gameParameters, trackChanges);
            var gamesDto = _mapper.Map<IEnumerable<BasicGameDto>>(gamesWithMetaData);

            return (games: gamesDto, metaData: gamesWithMetaData.MetaData);
        }

        // Get a single game with detailed information
        public async Task<GameDto> GetGameAsync(int gameId, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameDto = _mapper.Map<GameDto>(game);

            return gameDto;
        }

        // Get a collection of games by ids with detailed information
        public async Task<IEnumerable<GameDto>> GetGamesByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var idsList = ids.ToList();

            var games = await _repository.Game.GetGamesByIdsAsync(idsList, trackChanges);
            if (idsList.Count() != games.Count())
                throw new CollectionByIdsBadRequestException();

            var gamesToReturn = _mapper.Map<IEnumerable<GameDto>>(games);

            return gamesToReturn;
        }

        // POST

        // Create a single game with the ability to add children (developers, genres)
        public async Task<GameDto> CreateGameAsync(GameForCreationDto game)
        {
            var gameEntity = _mapper.Map<Game>(game);

            _repository.Game.CreateGame(gameEntity);
            await _repository.SaveAsync();

            // Create children
            // GameDevelopers table
            if (game.Developers != null)
            {
                foreach (var developer in game.Developers)
                {
                    var developerEntity = await _repository.Developer.GetDeveloperAsync(developer.DeveloperId, false);
                    if (developerEntity is null)
                        throw new DeveloperNotFoundException(developer.DeveloperId);

                    _repository.GameDevelopers.AddDeveloperToGame(gameEntity.Id, new GameDevelopers{DeveloperId = developer.DeveloperId});
                }
            }
            // GameGenres table
            if (game.Genres != null)
            {
                foreach (var genre in game.Genres)
                {
                    var genreEntity = await _repository.Genre.GetGenreAsync(genre.GenreId, false);
                    if (genreEntity is null)
                        throw new GenreNotFoundException(genre.GenreId);

                    _repository.GameGenres.AddGenreToGame(gameEntity.Id, new GameGenres { GenreId = genre.GenreId });
                }
            }

            await _repository.SaveAsync();

            var gameToReturn = _mapper.Map<GameDto>(gameEntity);

            return gameToReturn;
        }

        // Create a collection of games with the ability to add children (developers, genres)
        public async Task<(IEnumerable<GameDto> games, string ids)> CreateGamesCollectionAsync(
            IEnumerable<GameForCreationDto> gamesCollection)
        {

            var gamesCollectionList = gamesCollection.ToList();
            var games = _mapper.Map<List<Game>>(gamesCollection);

            // Add all games first
            foreach (var game in games)
            {
                _repository.Game.CreateGame(game);
            }
            await _repository.SaveAsync();

            for (int i = 0; i < games.Count; i++)
            {
                var gameDto = gamesCollectionList[i];
                var gameEntity = games[i];

                // Add Developers
                if (gameDto.Developers != null)
                {
                    foreach (var developer in gameDto.Developers)
                    {
                        var developerEntity = await _repository.Developer.GetDeveloperAsync(developer.DeveloperId, false);
                        if (developerEntity is null)
                            throw new DeveloperNotFoundException(developer.DeveloperId);

                        _repository.GameDevelopers.AddDeveloperToGame(gameEntity.Id,
                            new GameDevelopers { DeveloperId = developer.DeveloperId });

                    }
                }

                if (gameDto.Genres != null)
                {
                    foreach (var genre in gameDto.Genres)
                    {
                        var genreEntity = await _repository.Genre.GetGenreAsync(genre.GenreId, false);
                        if (genreEntity is null)
                            throw new GenreNotFoundException(genre.GenreId);

                        _repository.GameGenres.AddGenreToGame(gameEntity.Id, new GameGenres { GenreId = genre.GenreId });
                    }
                }
            }

            await _repository.SaveAsync();

            var gamesCollectionToReturn = _mapper.Map<IEnumerable<GameDto>>(games).ToList();
            var gameIds = string.Join(",", gamesCollectionToReturn.Select(g => g.Id));

            return (games: gamesCollectionToReturn, ids: gameIds);
        }

        // DELETE

        public async Task DeleteGameAsync(int gameId, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            _repository.Game.DeleteGame(game);
            await _repository.SaveAsync();
        }

        // UPDATE

        // Update a game, providing children will replace them with the provided ones, if not provided, they will be unchanged.
        public async Task UpdateGameAsync(int gameId, GameForUpdateDto gameForUpdate, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            _mapper.Map(gameForUpdate, game);

            // Update developers
            if (gameForUpdate.Developers != null)
            {
                // Remove existing developers from the game, not sure if this is good practice or not for a PUT request
                var existingGameDevelopers = await _repository.GameDevelopers.GetGameDevelopersAsync(gameId, false);
                foreach (var developer in existingGameDevelopers)
                {
                    _repository.GameDevelopers.RemoveDeveloperFromGame(developer);
                }

                // Add new developers
                foreach (var developer in gameForUpdate.Developers)
                {
                    var developerEntity = await _repository.Developer.GetDeveloperAsync(developer.DeveloperId, false);
                    if (developerEntity is null)
                        throw new DeveloperNotFoundException(developer.DeveloperId);

                    _repository.GameDevelopers.AddDeveloperToGame(gameId, new GameDevelopers { DeveloperId = developer.DeveloperId });
                }
            }

            // Update genres
            if (gameForUpdate.Genres != null)
            {
                // Remove existing genres from the game, not sure if this is good practice or not for a PUT request
                var existingGameGenres = await _repository.GameGenres.GetGameGenresAsync(gameId, false);

                foreach (var genre in existingGameGenres)
                {
                    _repository.GameGenres.RemoveGenreFromGame(genre);
                }

                // Add new genres
                foreach (var genre in gameForUpdate.Genres)
                {
                    var genreEntity = await _repository.Genre.GetGenreAsync(genre.GenreId, false);
                    if (genreEntity is null)
                        throw new GenreNotFoundException(genre.GenreId);

                    _repository.GameGenres.AddGenreToGame(gameId, new GameGenres { GenreId = genre.GenreId });
                }
            }

            await _repository.SaveAsync();
        }
    }
}
