using Shared.DataTransferObjects.Franchise;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service.Contracts
{
    public interface IFranchiseService
    {
        // GET
        Task<(IEnumerable<FranchiseDto> franchises, MetaData metaData)> GetAllFranchisesAsync(FranchiseParameters franchiseParameters, bool trackChanges);
        Task<FranchiseDto?> GetFranchiseAsync(int franchiseId, bool trackChanges);
        Task<IEnumerable<FranchiseDto>> GetFranchisesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        Task<FranchiseDto> CreateFranchiseAsync(FranchiseForCreationDto franchise);
        Task<(IEnumerable<FranchiseDto> franchises, string ids)> CreateFranchisesCollectionAsync(IEnumerable<FranchiseForCreationDto> franchisesCollection);
        // DELETE
        Task DeleteFranchiseAsync(int franchiseId, bool trackChanges);
    }
}
