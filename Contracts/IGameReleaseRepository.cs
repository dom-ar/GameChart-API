using Entities.Models;
using Entities.Models.Joint_Models;

namespace Contracts
{
    public interface IGameReleaseRepository
    {
        // GET
        Task<IEnumerable<GameRelease>> GetGameReleasesAsync(int gameId, bool trackChanges);
        Task<GameRelease> GetGameReleaseAsync(int gameId, int id, bool trackChanges);
        Task<IEnumerable<GameRelease>> GetGameReleasesByIdsAsync(int gameId, IEnumerable<int> ids, bool trackChanges);
        // POST
        void CreateReleaseForGame(int gameId, GameRelease gameRelease);
        // DELETE
        void DeleteReleaseFromGame(GameRelease gameRelease);
    }
}
