using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Repository
{
    public class DlcRepository : RepositoryBase<Dlc>, IDlcRepository
    {
        public DlcRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Dlc>> GetDlcsAsync(int gameId, DlcParameters dlcParameters, bool trackChanges)
        {
            var gameDlc = await FindByCondition(d => d.GameId.Equals(gameId), trackChanges)
                .Include(d => d.Publisher)
                .Include(d => d.DlcDevelopers).ThenInclude(dd => dd.Developer)
                .Include(d => d.DlcGenres).ThenInclude(dg => dg.Genre)
                .OrderBy(d => d.Title)
                .Skip((dlcParameters.PageNumber - 1) * dlcParameters.PageSize)
                .Take(dlcParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(d => d.GameId.Equals(gameId), trackChanges).CountAsync();

            return new PagedList<Dlc>(gameDlc, count, dlcParameters.PageNumber, dlcParameters.PageSize);
        }


        public async Task<PagedList<Dlc>> GetAllDlcsAsync(DlcParameters dlcParameters, bool trackChanges)
        {
            var dlc = await FindAll(trackChanges)
                .Include(d => d.Publisher)
                .Include(d => d.DlcDevelopers).ThenInclude(dd => dd.Developer)
                .Include(d => d.DlcGenres).ThenInclude(dg => dg.Genre)
                .OrderBy(d => d.Title)
                .Skip((dlcParameters.PageNumber - 1) * dlcParameters.PageSize)
                .Take(dlcParameters.PageSize)
                .ToListAsync();

            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<Dlc>(dlc, count, dlcParameters.PageNumber, dlcParameters.PageSize);
        }

        public async Task<Dlc?> GetDlcAsync(int gameId, int id, bool trackChanges) =>
            await FindByCondition(d => d.GameId.Equals(gameId) && d.Id.Equals(id), trackChanges)
                .Include(d => d.Publisher)
                .Include(d => d.DlcDevelopers).ThenInclude(dd => dd.Developer)
                .Include(d => d.DlcGenres).ThenInclude(dg => dg.Genre)
                .SingleOrDefaultAsync();

        public void CreateDlcForGame(int gameId, Dlc dlc)
        {
            dlc.GameId = gameId;
            Create(dlc);
        }

        public void DeleteDlcFromGame(int gameId, Dlc dlc)
        {
            dlc.GameId = gameId;
            Delete(dlc);
        }
    }
}