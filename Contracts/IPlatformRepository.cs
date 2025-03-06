using Entities.Models;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Contracts
{
    public interface IPlatformRepository
    {
        // GET
        Task<PagedList<Platform>> GetAllPlatformsAsync(PlatformParameters platformParameters, bool trackChanges);
        Task<Platform?> GetPlatformAsync(int platformId, bool trackChanges);
        Task<IEnumerable<Platform>> GetPlatformsByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        void CreatePlatform(Platform platform);
        // DELETE
        void DeletePlatform(Platform platform);
    }
}
