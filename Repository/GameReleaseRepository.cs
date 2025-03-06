using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GameReleaseRepository : RepositoryBase<GameRelease>, IGameReleaseRepository
    {
        public GameReleaseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<GameRelease>> GetGameReleasesAsync(int gameId, bool trackChanges) =>
            await FindByCondition(gr => gr.GameId.Equals(gameId), trackChanges)
                .OrderBy(gr => gr.ReleaseDate)
                .ToListAsync();

        public async Task<GameRelease> GetGameReleaseAsync(int gameId, int id, bool trackChanges) =>
            await FindByCondition(gr => gr.GameId.Equals(gameId) && gr.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<GameRelease>> GetGameReleasesByIdsAsync(int gameId, IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(gr => gr.GameId.Equals(gameId) && ids.Contains(gr.Id), trackChanges)
                .ToListAsync();

        public void CreateReleaseForGame(int gameId, GameRelease gameRelease)
        {
            gameRelease.GameId = gameId;
            Create(gameRelease);
        }

        public void DeleteReleaseFromGame(GameRelease gameRelease) => Delete(gameRelease);
    }
}