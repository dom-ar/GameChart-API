using Shared.DataTransferObjects.GameDevelopers;

namespace Service.Contracts
{
    public interface IGameDevelopersService
    {
        // GET
        Task<IEnumerable<GameDevelopersDto>> GetGameDevelopersAsync(int gameId, bool trackChanges);
        Task<IEnumerable<GameDevelopersDto>> GetDeveloperGamesAsync(int developerId, bool trackChanges);
        Task<IEnumerable<GameDevelopersDto>> GetGameDevelopersByIdsAsync(int gameId, IEnumerable<int> ids, bool trackChanges);
        // POST
        Task<GameDevelopersDto> AddDeveloperToGameAsync(int gameId, GameDevelopersForCreationDto gameDevelopers, bool trackChanges);

        Task<(IEnumerable<GameDevelopersDto> gameDevelopers, string ids)> AddGameDevelopersCollectionAsync(int gameId,
            IEnumerable<GameDevelopersForCreationDto> gameDevelopersCollection, bool trackChanges);
        // DELETE
        Task RemoveDeveloperFromGameAsync(int gameId, int developerId, bool trackChanges);
    }
}