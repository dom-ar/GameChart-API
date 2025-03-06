using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class DlcReleaseRepository : RepositoryBase<DlcRelease>, IDlcReleaseRepository
    {
        public DlcReleaseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<DlcRelease>> GetDlcReleasesAsync(int dlcId, bool trackChanges) =>
            await FindByCondition(dr => dr.DlcId.Equals(dlcId), trackChanges)
                .OrderBy(dr => dr.ReleaseDate)
                .ToListAsync();

        public async Task<DlcRelease> GetDlcReleaseAsync(int dlcId, int id, bool trackChanges) =>
            await FindByCondition(dr => dr.DlcId.Equals(dlcId) && dr.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<DlcRelease>> GetDlcReleasesByIdsAsync(int dlcId, IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(dr => dr.DlcId.Equals(dlcId) && ids.Contains(dr.Id), trackChanges)
                .ToListAsync();

        public void CreateReleaseForDlc(int dlcId, DlcRelease dlcRelease)
        {
            dlcRelease.DlcId = dlcId;
            Create(dlcRelease);
        }

        public void DeleteReleaseFromDlc(DlcRelease dlcRelease) => Delete(dlcRelease);

    }
}