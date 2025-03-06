using System.Linq;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Repository
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        // GET

        public async Task<PagedList<Game>> GetAllGamesAsync(GameParameters gameParameters, bool trackChanges)
        {
            var query = FindAll(trackChanges)
                .Include(g => g.Publisher)
                .Include(g => g.GameDevelopers).ThenInclude(gd => gd.Developer)
                .Include(g => g.GameGenres).ThenInclude(gg => gg.Genre)
                .AsQueryable();

            if (gameParameters.FranchiseIds.Any())
            {
                query = query.Where(g => 
                    g.FranchiseId.HasValue &&
                    gameParameters.FranchiseIds.Contains(g.FranchiseId.Value));
            }

            if (gameParameters.EngineIds.Any())
            {
                query = query.Where(g =>
                    g.EngineId.HasValue &&
                    gameParameters.EngineIds.Contains(g.EngineId.Value));
            }

            if (gameParameters.PublisherIds.Any())
            {
                query = query.Where(g =>
                    g.PublisherId.HasValue &&
                    gameParameters.PublisherIds.Contains(g.PublisherId.Value));
            }

            if (gameParameters.DeveloperIds.Any())
            {
                query = query.Where(g =>
                    g.GameDevelopers.Any(gd => gameParameters.DeveloperIds.Contains(gd.DeveloperId)));
            }

            if (gameParameters.GenreIds.Any())
            {
                query = query.Where(g =>
                    g.GameGenres.Any(gg => gameParameters.GenreIds.Contains(gg.GenreId)));
            }

            query = query.Where(g =>
                g.ReleasedOnYear.HasValue && 
                g.ReleasedOnYear >= gameParameters.MinYear && 
                g.ReleasedOnYear <= gameParameters.MaxYear);

            var count = await query.CountAsync();

            var games = await query
                .OrderBy(g => g.Id)
                .Skip((gameParameters.PageNumber - 1) * gameParameters.PageSize)
                .Take(gameParameters.PageSize)
                .ToListAsync();

            return new PagedList<Game>(games, count, gameParameters.PageNumber,
                gameParameters.PageSize);
        }

        public async Task<Game?> GetGameAsync(int gameId, bool trackChanges) =>
            await FindByCondition(g => g.Id.Equals(gameId), trackChanges)
                .Include(g => g.Publisher)
                .Include(g => g.Franchise)
                .Include(g => g.Engine)
                .Include(g => g.Dlc)
                .Include(g => g.GameDevelopers).ThenInclude(gd => gd.Developer)
                .Include(g => g.GameGenres).ThenInclude(gg => gg.Genre)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Game>> GetGamesByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(g => ids.Contains(g.Id), trackChanges)
            .ToListAsync();

        // POST
        public void CreateGame(Game game) => Create(game);

        // DELETE
        public void DeleteGame(Game game) => Delete(game);
    }
}