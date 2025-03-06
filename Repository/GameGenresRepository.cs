using Contracts;
using Entities.Models.Joint_Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GameGenresRepository : RepositoryBase<GameGenres>, IGameGenresRepository
    {
        public GameGenresRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<GameGenres>> GetGameGenresAsync(int gameId, bool trackChanges) =>
            await FindByCondition(gg => gg.GameId.Equals(gameId), trackChanges)
                .OrderBy(gg => gg.Genre!.Name)
                .ToListAsync();

        public async Task<IEnumerable<GameGenres>> GetGenreGamesAsync(int genreId, bool trackChanges) =>
            await FindByCondition(gg => gg.GenreId.Equals(genreId), trackChanges)
                .OrderBy(gg => gg.Game!.Title)
                .ToListAsync();

        public async Task<IEnumerable<GameGenres>> GetGameGenresByIdsAsync(int gameId, IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(gg => gg.GameId.Equals(gameId) && ids.Contains(gg.GenreId), trackChanges)
                .ToListAsync();


        public void AddGenreToGame(int gameId, GameGenres gameGenres)
        {
            gameGenres.GameId = gameId;
            Create(gameGenres);
        }

        public void RemoveGenreFromGame(GameGenres gameGenres) => Delete(gameGenres);
    }
}