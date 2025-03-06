using Shared.DataTransferObjects.GameRelease;

namespace Service.Contracts
{
    public interface IGameReleaseService
    {
        // GET
        Task<IEnumerable<GameReleaseDto>> GetGameReleasesAsync(int gameId, bool trackChanges);
        Task<GameReleaseDto> GetGameReleaseAsync(int gameId, int id, bool trackChanges);
        Task<IEnumerable<GameReleaseDto>> GetGameReleasesByIdsAsync(int gameId, IEnumerable<int> ids, bool trackChanges);
        // POST
        Task<GameReleaseDto> CreateReleaseForGameAsync(int gameId, GameReleaseCreationDto gameReleaseCreation, bool trackChanges);

        Task<(IEnumerable<GameReleaseDto> gameRelease, string ids)> CreateGameReleasesCollectionAsync(int gameId,
            IEnumerable<GameReleaseCreationDto> gameReleasesCollection, bool trackChanges);
        // DELETE
        Task DeleteReleaseFromGameAsync(int gameId, int id, bool trackChanges);
    }
}
