using Contracts;
using Entities.Models.Joint_Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class DlcGenresRepository : RepositoryBase<DlcGenres>, IDlcGenresRepository
    {
        public DlcGenresRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<DlcGenres>> GetDlcGenresAsync(int dlcId, bool trackChanges) =>
            await FindByCondition(dg => dg.DlcId.Equals(dlcId), trackChanges)
                .OrderBy(dg => dg.Genre!.Name)
                .ToListAsync();

        public void AddGenreToDlc(int dlcId, DlcGenres dlcGenres)
        {
            dlcGenres.DlcId = dlcId;
            Create(dlcGenres);
        }

        public void RemoveGenreFromDlc(DlcGenres dlcGenres) => Delete(dlcGenres);

    }
}