using System.Linq;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Linq.Dynamic.Core;
using Repository.Extensions;


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

            if (gameParameters.MinYear.HasValue)
            {
                const int max = int.MaxValue;
                query = query.Where(g =>
                    g.ReleasedOnYear.HasValue &&
                    g.ReleasedOnYear >= gameParameters.MinYear &&
                    g.ReleasedOnYear <= max);
            }

            if (gameParameters.MaxYear.HasValue)
            {
                const int min = 0;
                query = query.Where(g =>
                    g.ReleasedOnYear.HasValue &&
                    g.ReleasedOnYear <= gameParameters.MaxYear &&
                    g.ReleasedOnYear >= min);
            }

            if (!string.IsNullOrWhiteSpace(gameParameters.SearchTerm))
            {
                var searchTerm = gameParameters.SearchTerm.Trim();
                // add .ToLower(); if using trigrams

                query = query.Where(g => EF.Functions.ILike(g.Title ?? "", $"%{searchTerm}%"));

                // To be used for full web search, not individual
                //query = query.Where(g => EF.Functions.ILike(g.Title ?? "", $"%{searchTerm}%") ||
                //                        EF.Functions.TrigramsSimilarity(EF.Property<string>(g, "Title").ToLower(), searchTerm) > 0.1);
            }

            query = query.Sort(gameParameters.OrderBy);

            var count = await query.CountAsync();

            var games = await query
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