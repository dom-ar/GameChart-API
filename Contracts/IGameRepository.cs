using Entities.Models;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Contracts
{
    public interface IGameRepository
    {
        // GET
        Task<PagedList<Game>> GetAllGamesAsync(GameParameters gameParameters, bool trackChanges);
        Task<Game?> GetGameAsync(int gameId, bool trackChanges);
        Task<IEnumerable<Game>> GetGamesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        void CreateGame(Game game);
        // DELETE
        void DeleteGame(Game game);
    }
}
