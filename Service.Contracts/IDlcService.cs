using Shared.DataTransferObjects.Dlc;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service.Contracts
{
    public interface IDlcService
    {
        // GET
        Task<(IEnumerable<DlcDto> dlcs, MetaData metaData)> GetDlcsAsync(int gameId, DlcParameters dlcParameters, bool trackChanges);
        Task<(IEnumerable<DlcDto> dlcs, MetaData metaData)> GetAllDlcsAsync(DlcParameters dlcParameters, bool trackChanges);
        Task<DlcDto> GetDlcAsync(int gameId, int id, bool trackChanges);
        // POST
        Task<DlcDto> CreateDlcForGameAsync(int gameId, DlcForCreationDto dlcForCreation, bool trackChanges);

        Task<(IEnumerable<DlcDto> dlcs, string ids)> CreateDlcCollectionForGameAsync(int gameId,
            IEnumerable<DlcForCreationDto> dlcsCollection, bool trackChanges);
        // PUT
        Task UpdateDlcAsync(int gameId, int id, DlcForUpdateDto dlcForUpdate, bool gameTrackChanges, bool dlcTrackChanges);
        // DELETE
        Task DeleteDlcFromGameAsync(int gameId, int dlcId, bool trackChanges);
    }
}
