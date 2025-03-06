using Entities.Models;

namespace Contracts
{
    public interface IDlcReleaseRepository
    {
        // GET
        Task<IEnumerable<DlcRelease>> GetDlcReleasesAsync(int dlcId, bool trackChanges);
        Task<DlcRelease> GetDlcReleaseAsync(int dlcId, int id, bool trackChanges);
        Task<IEnumerable<DlcRelease>> GetDlcReleasesByIdsAsync(int dlcId, IEnumerable<int> ids, bool trackChanges);
        // POST
        void CreateReleaseForDlc(int dlcId, DlcRelease dlcRelease);
        // DELETE
        void DeleteReleaseFromDlc(DlcRelease dlcRelease);
    }
}
