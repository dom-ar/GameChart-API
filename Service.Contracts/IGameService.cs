using Shared.DataTransferObjects.Game;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service.Contracts
{
    public interface IGameService
    {
        // GET
        Task<(IEnumerable<BasicGameDto> games, MetaData metaData)> GetAllGamesAsync(GameParameters gameParameters, bool trackChanges);
        Task<GameDto> GetGameAsync(int gameId, bool trackChanges);
        Task<IEnumerable<GameDto>> GetGamesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        Task<GameDto> CreateGameAsync(GameForCreationDto game);
        Task<(IEnumerable<GameDto> games, string ids)> CreateGamesCollectionAsync(IEnumerable<GameForCreationDto> gamesCollection);
        // DELETE
        Task DeleteGameAsync(int gameId, bool trackChanges);
        // PUT
        Task UpdateGameAsync(int gameId, GameForUpdateDto gameForUpdate, bool trackChanges);
    }
}
