using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models.Joint_Models;
using Service.Contracts;
using Shared.DataTransferObjects.GameDevelopers;

namespace Service
{
    internal sealed class GameDevelopersService : IGameDevelopersService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public GameDevelopersService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameDevelopersDto>> GetGameDevelopersAsync(int gameId, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameDevelopers = await _repository.GameDevelopers.GetGameDevelopersAsync(gameId, trackChanges);
            var gameDevelopersDto = _mapper.Map<IEnumerable<GameDevelopersDto>>(gameDevelopers);

            return gameDevelopersDto;
        }

        public async Task<IEnumerable<GameDevelopersDto>> GetDeveloperGamesAsync(int developerId, bool trackChanges)
        {
            var developer = await _repository.Developer.GetDeveloperAsync(developerId, trackChanges);
            if (developer is null)
                throw new DeveloperNotFoundException(developerId);

            var developerGames = await _repository.GameDevelopers.GetDeveloperGamesAsync(developerId, trackChanges);
            var developerGamesDto = _mapper.Map<IEnumerable<GameDevelopersDto>>(developerGames);

            return developerGamesDto;
        }

        public async Task<IEnumerable<GameDevelopersDto>> GetGameDevelopersByIdsAsync(int gameId, IEnumerable<int> ids,
            bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var idsList = ids.ToList();

            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameDevelopers = await _repository.GameDevelopers.GetGameDevelopersCollectionAsync(gameId, idsList, trackChanges);
            if (gameDevelopers.Count() != idsList.Count)
                throw new CollectionByIdsBadRequestException();

            var gameDevelopersToReturn = _mapper.Map<IEnumerable<GameDevelopersDto>>(gameDevelopers);
            return gameDevelopersToReturn;
        }

        public async Task<GameDevelopersDto> AddDeveloperToGameAsync(int gameId, GameDevelopersForCreationDto gameDevelopers, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var developer = await _repository.Developer.GetDeveloperAsync(gameDevelopers.DeveloperId, trackChanges);
            if (developer is null)
                throw new DeveloperNotFoundException(gameDevelopers.DeveloperId);

            var gameDevelopersEntity = _mapper.Map<GameDevelopers>(gameDevelopers);

            _repository.GameDevelopers.AddDeveloperToGame(gameId, gameDevelopersEntity);
            await _repository.SaveAsync();

            var gameDevsToReturn = _mapper.Map<GameDevelopersDto>(gameDevelopersEntity);

            return gameDevsToReturn;
        }

        public async Task<(IEnumerable<GameDevelopersDto> gameDevelopers, string ids)> AddGameDevelopersCollectionAsync(int gameId,
            IEnumerable<GameDevelopersForCreationDto> gameDevelopersCollection, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameDevelopersCollectionList = gameDevelopersCollection.ToList();
            var gameDevelopersEntities = _mapper.Map<IEnumerable<GameDevelopers>>(gameDevelopersCollectionList);

            foreach (var developer in gameDevelopersCollectionList)
            {
                var developerEntity = await _repository.Developer.GetDeveloperAsync(developer.DeveloperId, trackChanges);
                if (developerEntity is null)
                    throw new DeveloperNotFoundException(developer.DeveloperId);
            }

            foreach (var gameDevelopersEntity in gameDevelopersEntities)
            {
                _repository.GameDevelopers.AddDeveloperToGame(gameId, gameDevelopersEntity);
            }

            await _repository.SaveAsync();

            var gameDevelopersToReturn = _mapper.Map<IEnumerable<GameDevelopersDto>>(gameDevelopersEntities).ToList();
            var ids = string.Join(",", gameDevelopersToReturn.Select(gd => gd.Developer));

            return (gameDevelopersToReturn, ids);
        }

        public async Task RemoveDeveloperFromGameAsync(int gameId, int developerId, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            //var developer = await _repository.Developer.GetDeveloperAsync(developerId, trackChanges);
            //if (developer is null)
            //    throw new DeveloperNotFoundException(developerId);

            //var gameDeveloperEntity = await _repository.GameDevelopers.GetGameDevelopersAsync(gameId, trackChanges)
            //    .FirstOrDefault(gd => gd.DeveloperId == developerId);
            //if (gameDeveloperEntity is null)
            //    throw new GameDeveloperNotFoundException(gameId, developerId);

            //_repository.GameDevelopers.RemoveDeveloperFromGame(gameDeveloperEntity);
            //await _repository.SaveAsync();
        }
    }
}