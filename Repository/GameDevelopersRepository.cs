using Contracts;
using Entities.Models.Joint_Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GameDevelopersRepository : RepositoryBase<GameDevelopers>, IGameDevelopersRepository
    {
        public GameDevelopersRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<GameDevelopers>> GetGameDevelopersAsync(int gameId, bool trackChanges) =>
            await FindByCondition(gd => gd.GameId.Equals(gameId), trackChanges)
                .Include(gg => gg.Developer)
                .OrderBy(gd => gd.Developer!.Name)
                .ToListAsync();

        public async Task<IEnumerable<GameDevelopers>> GetDeveloperGamesAsync(int developerId, bool trackChanges) =>
            await FindByCondition(gd => gd.DeveloperId.Equals(developerId), trackChanges)
                .OrderBy(gd => gd.Game!.Title)
                .ToListAsync();

        public async Task<IEnumerable<GameDevelopers>> GetGameDevelopersCollectionAsync(int gameId, IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(gd => gd.GameId.Equals(gameId) && ids.Contains(gd.DeveloperId), trackChanges)
                .ToListAsync();

        public void AddDeveloperToGame(int gameId, GameDevelopers gameDevelopers)
        {
            gameDevelopers.GameId = gameId;
            Create(gameDevelopers);
        }

        public void RemoveDeveloperFromGame(GameDevelopers gameDevelopers) => Delete(gameDevelopers);

    }
}