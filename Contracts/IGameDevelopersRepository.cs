using Entities.Models.Joint_Models;

namespace Contracts
{
    public interface IGameDevelopersRepository
    {
        // GET
        Task<IEnumerable<GameDevelopers>> GetGameDevelopersAsync(int gameId, bool trackChanges);
        Task<IEnumerable<GameDevelopers>> GetDeveloperGamesAsync(int developerId, bool trackChanges);
        Task<IEnumerable<GameDevelopers>> GetGameDevelopersCollectionAsync(int gameId, IEnumerable<int> ids, bool trackChanges);
        // POST
        void AddDeveloperToGame(int gameId, GameDevelopers gameDevelopers);
        // DELETE
        void RemoveDeveloperFromGame(GameDevelopers gameDevelopers);
    }
}