using Entities.Models;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Contracts
{
    public interface IDlcRepository
    {
        // GET
        Task<PagedList<Dlc>> GetDlcsAsync(int gameId, DlcParameters dlcParameters, bool trackChanges);
        Task<PagedList<Dlc>> GetAllDlcsAsync(DlcParameters dlcParameters, bool trackChanges);
        Task<Dlc?> GetDlcAsync(int gameId, int id, bool trackChanges);
        // POST
        void CreateDlcForGame(int gameId, Dlc dlc);
        // DELETE
        void DeleteDlcFromGame(int gameId, Dlc dlc);
    }
}
