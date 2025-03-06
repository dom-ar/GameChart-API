using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Platform;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service.Contracts
{
    public interface IPlatformService
    {
        // GET
        Task<(IEnumerable<PlatformDto> platforms, MetaData metaData)> GetAllPlatformsAsync(PlatformParameters platformParameters, bool trackChanges);
        Task<PlatformDto> GetPlatformAsync(int platformId, bool trackChanges);
        Task<IEnumerable<PlatformDto>> GetPlatformsByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        Task<PlatformDto> CreatePlatformAsync(PlatformForCreationDto platform);
        Task<(IEnumerable<PlatformDto> platforms, string ids)> CreatePlatformsCollectionAsync(IEnumerable<PlatformForCreationDto> platformsCollection);
        // DELETE
        Task DeletePlatformAsync(int platformId, bool trackChanges);
    }
}
