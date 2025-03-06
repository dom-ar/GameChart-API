using Shared.DataTransferObjects.DlcRelease;

namespace Service.Contracts
{
    public interface IDlcReleaseService
    {
        // GET
        Task<IEnumerable<DlcReleaseDto>> GetDlcReleasesAsync(int gameId, int dlcId, bool trackChanges);
        Task<DlcReleaseDto> GetDlcReleaseAsync(int gameId,int dlcId, int id, bool trackChanges);
        Task<IEnumerable<DlcReleaseDto>> GetDlcReleasesByIdsAsync(int gameId, int dlcId, IEnumerable<int> ids, bool trackChanges);
        // POST
        Task<DlcReleaseDto> CreateReleaseForDlcAsync(int gameId, int dlcId, DlcReleaseCreationDto dlcReleaseCreation, bool trackChanges);
        Task<(IEnumerable<DlcReleaseDto> dlcReleases, string ids)> CreateDlcReleasesCollectionAsync(int gameId, int dlcId,
            IEnumerable<DlcReleaseCreationDto> dlcReleasesCollection, bool trackChanges);
        // DELETE
        Task DeleteReleaseFromDlcAsync(int gameId, int dlcId, int id, bool trackChanges);
    }
}
