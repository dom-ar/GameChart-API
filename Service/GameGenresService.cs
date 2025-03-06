using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models.Joint_Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.GameGenres;

namespace Service
{
    internal sealed class GameGenresService : IGameGenresService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public GameGenresService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameGenresDto>> GetGameGenresAsync(int gameId, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameGenres = await _repository.GameGenres.GetGameGenresAsync(gameId, trackChanges);
            var gameGenresDto = _mapper.Map<IEnumerable<GameGenresDto>>(gameGenres);

            return gameGenresDto;
        }

        public async Task<IEnumerable<GameGenresDto>> GetGenreGamesAsync(int genreId, bool trackChanges)
        {
            var genre = await _repository.Genre.GetGenreAsync(genreId, trackChanges);
            if (genre is null)
                throw new GenreNotFoundException(genreId);

            var genreGames = await _repository.GameGenres.GetGenreGamesAsync(genreId, trackChanges);
            var genreGamesDto = _mapper.Map<IEnumerable<GameGenresDto>>(genreGames);

            return genreGamesDto;
        }

        public async Task<IEnumerable<GameGenresDto>> GetGameGenresByIdsAsync(int gameId, IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var idsList = ids.ToList();

            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameGenres = await _repository.GameGenres.GetGameGenresByIdsAsync(gameId, idsList, trackChanges);
            if (idsList.Count() != gameGenres.Count())
                throw new CollectionByIdsBadRequestException();

            var gameGenresToReturn = _mapper.Map<IEnumerable<GameGenresDto>>(gameGenres);
            return gameGenresToReturn;
        }

        public async Task<GameGenresDto> AddGenreToGameAsync(int gameId, GameGenresForCreationDto gameGenres, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var genre = await _repository.Genre.GetGenreAsync(gameGenres.GenreId, trackChanges);
            if (genre is null)
                throw new GenreNotFoundException(gameGenres.GenreId);

            var gameGenresEntity = _mapper.Map<GameGenres>(gameGenres);

            _repository.GameGenres.AddGenreToGame(gameId, gameGenresEntity);
            await _repository.SaveAsync();

            var gameGenresToReturn = _mapper.Map<GameGenresDto>(gameGenresEntity);
            return gameGenresToReturn;
        }

        public async Task<(IEnumerable<GameGenresDto> gameGenres, string ids)> AddGamesGenresCollectionAsync(int gameId,
            IEnumerable<GameGenresForCreationDto> gameGenres, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, false);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameGenresCollectionList = gameGenres.ToList();
            var gameGenresEntities = _mapper.Map<IEnumerable<GameGenres>>(gameGenresCollectionList);

            foreach (var genre in gameGenresCollectionList)
            {
                var genreEntity = await _repository.Genre.GetGenreAsync(genre.GenreId, trackChanges);
                if (genreEntity is null)
                    throw new GenreNotFoundException(genre.GenreId);
            }

            foreach (var gameGenresEntity in gameGenresEntities)
            {
                _repository.GameGenres.AddGenreToGame(gameId, gameGenresEntity);
            }

            await _repository.SaveAsync();

            var gameGenresToReturn = _mapper.Map<IEnumerable<GameGenresDto>>(gameGenresEntities).ToList();
            var ids = string.Join(", ", gameGenresToReturn.Select(g => g.GenreId));

            return (gameGenresToReturn, ids);

        }

        public async Task RemoveGenreFromGameAsync(int gameId, int genreId, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            //var genre = await _repository.Genre.GetGenreAsync(genreId, trackChanges);
            //if (genre is null)
            //    throw new GenreNotFoundException(genreId);

            //var gameGenresEntity =  await _repository.GameGenres.GetGameGenresAsync(gameId, trackChanges)
            //    .FirstOrDefault(g => g.GenreId == genreId);
            //if (gameGenresEntity is null)
            //    throw new GameGenreNotFoundException(gameId, genreId);

            //_repository.GameGenres.RemoveGenreFromGame(gameGenresEntity);
            //await _repository.SaveAsync();
        }

    }
}