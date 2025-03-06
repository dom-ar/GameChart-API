using Entities.Models.Joint_Models;

namespace Contracts
{
    public interface IDlcGenresRepository
    {
        // GET
        Task<IEnumerable<DlcGenres>> GetDlcGenresAsync(int dlcId, bool trackChanges);
        // POST
        void AddGenreToDlc(int dlcId, DlcGenres dlcGenres);
        // DELETE
        void RemoveGenreFromDlc(DlcGenres dlcGenres);
    }
}