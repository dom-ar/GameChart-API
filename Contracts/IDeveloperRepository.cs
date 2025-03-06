using Entities.Models;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Contracts
{
    public interface IDeveloperRepository
    {
        // GET
        Task<PagedList<Developer>> GetAllDevelopersAsync(DeveloperParameters developerParameters, bool trackChanges);
        Task<Developer?> GetDeveloperAsync(int developerId, bool trackChanges);
        Task<IEnumerable<Developer>> GetDevelopersByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        void CreateDeveloper(Developer developer);
        // DELETE
        void DeleteDeveloper(Developer developer);

    }
}
