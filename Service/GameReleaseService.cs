using System.Security.Cryptography;
using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.GameRelease;

namespace Service
{
    internal sealed class GameReleaseService : IGameReleaseService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public GameReleaseService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameReleaseDto>> GetGameReleasesAsync(int gameId, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameReleases = await _repository.GameRelease.GetGameReleasesAsync(gameId, trackChanges);
            var gameReleasesDto = _mapper.Map<IEnumerable<GameReleaseDto>>(gameReleases);

            return gameReleasesDto;
        }

        public async Task<GameReleaseDto> GetGameReleaseAsync(int gameId, int id, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameRelease = await _repository.GameRelease.GetGameReleaseAsync(gameId, id, trackChanges);
            if (gameRelease is null)
                throw new GameReleaseNotFoundException(id);

            var gameReleaseDto = _mapper.Map<GameReleaseDto>(gameRelease);
            return gameReleaseDto;
        }

        public async Task<IEnumerable<GameReleaseDto>> GetGameReleasesByIdsAsync(int gameId, IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var idsList = ids.ToList();

            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameReleases = await _repository.GameRelease.GetGameReleasesByIdsAsync(gameId, idsList, trackChanges);
            if (gameReleases.Count() != idsList.Count)
                throw new CollectionByIdsBadRequestException();

            var gameReleasesToReturn = _mapper.Map<IEnumerable<GameReleaseDto>>(gameReleases);
            return gameReleasesToReturn;
        }

        public async Task<GameReleaseDto> CreateReleaseForGameAsync(int gameId, GameReleaseCreationDto gameReleaseCreation, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameRelease = _mapper.Map<GameRelease>(gameReleaseCreation);

            _repository.GameRelease.CreateReleaseForGame(gameId, gameRelease);
            await _repository.SaveAsync();

            var gameReleaseDto = _mapper.Map<GameReleaseDto>(gameRelease);
            return gameReleaseDto;
        }

        public async Task<(IEnumerable<GameReleaseDto> gameRelease, string ids)> CreateGameReleasesCollectionAsync(int gameId,
            IEnumerable<GameReleaseCreationDto> gameReleasesCollection, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameReleasesEntities = _mapper.Map<IEnumerable<GameRelease>>(gameReleasesCollection);

            foreach (var gameRelease in gameReleasesEntities)
            {
                _repository.GameRelease.CreateReleaseForGame(gameId, gameRelease);
            }
            await _repository.SaveAsync();

            var gameReleasesToReturn = _mapper.Map<IEnumerable<GameReleaseDto>>(gameReleasesEntities).ToList();
            var ids = string.Join(",", gameReleasesToReturn.Select(x => x.Id));

            return (gameReleasesToReturn, ids);
        }

        public async Task DeleteReleaseFromGameAsync(int gameId, int id, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var gameReleaseEntity = await _repository.GameRelease.GetGameReleaseAsync(gameId, id, trackChanges);
            if (gameReleaseEntity is null)
                throw new GameReleaseNotFoundException(id);

            _repository.GameRelease.DeleteReleaseFromGame(gameReleaseEntity);
            await _repository.SaveAsync();
        }
    }
}
