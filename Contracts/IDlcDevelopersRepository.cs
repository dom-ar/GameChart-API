using Entities.Models.Joint_Models;

namespace Contracts
{
    public interface IDlcDevelopersRepository
    {
        // GET
        Task<IEnumerable<DlcDevelopers>> GetDlcDevelopersAsync(int dlcId, bool trackChanges);
        // POST
        void AddDeveloperToDlc(int dlcId, DlcDevelopers dlcDevelopers);
        // DELETE
        void RemoveDeveloperFromDlc(DlcDevelopers dlcDevelopers);
    }
}