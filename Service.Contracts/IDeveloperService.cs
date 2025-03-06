using Shared.DataTransferObjects.Developer;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service.Contracts
{
    public interface IDeveloperService
    {
        // GET
        Task<(IEnumerable<DeveloperDto> developers, MetaData metaData)> GetAllDevelopersAsync(DeveloperParameters developerParameters, bool trackChanges);
        Task<DeveloperDto> GetDeveloperAsync(int developerId, bool trackChanges);
        Task<IEnumerable<DeveloperDto>> GetDevelopersByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        Task<DeveloperDto> CreateDeveloperAsync(DeveloperForCreationDto developer);
        Task<(IEnumerable<DeveloperDto> developers, string ids)> CreateDevelopersCollectionAsync(
            IEnumerable<DeveloperForCreationDto> developersCollection);
        // DELETE
        Task DeleteDeveloperAsync(int developerId, bool trackChanges);
    }
}
