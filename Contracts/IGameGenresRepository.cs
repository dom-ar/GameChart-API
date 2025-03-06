using Entities.Models.Joint_Models;

namespace Contracts
{
    public interface IGameGenresRepository
    {
        // GET
        Task<IEnumerable<GameGenres>> GetGameGenresAsync(int gameId, bool trackChanges);
        Task<IEnumerable<GameGenres>> GetGenreGamesAsync(int genreId, bool trackChanges);
        Task<IEnumerable<GameGenres>> GetGameGenresByIdsAsync(int gameId, IEnumerable<int> ids, bool trackChanges);
        // POST
        void AddGenreToGame(int gameId, GameGenres gameGenres);
        // DELETE
        void RemoveGenreFromGame(GameGenres gameGenres);
    }
}