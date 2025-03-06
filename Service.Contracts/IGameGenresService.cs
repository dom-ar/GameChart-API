using Shared.DataTransferObjects;
using Shared.DataTransferObjects.GameGenres;

namespace Service.Contracts
{
    public interface IGameGenresService
    {
        // GET
        Task<IEnumerable<GameGenresDto>> GetGameGenresAsync(int gameId, bool trackChanges);
        Task<IEnumerable<GameGenresDto>> GetGenreGamesAsync(int genreId, bool trackChanges);
        Task<IEnumerable<GameGenresDto>> GetGameGenresByIdsAsync(int gameId, IEnumerable<int> ids, bool trackChanges);
        // POST
        Task<GameGenresDto> AddGenreToGameAsync(int gameId, GameGenresForCreationDto gameGenres, bool trackChanges);
        Task<(IEnumerable<GameGenresDto> gameGenres, string ids)> AddGamesGenresCollectionAsync(int gameId, IEnumerable<GameGenresForCreationDto> gameGenresCollection, bool trackChanges);
        // DELETE
        Task RemoveGenreFromGameAsync(int gameId, int genreId, bool trackChanges);
    }
}