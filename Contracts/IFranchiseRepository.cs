using Entities.Models;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Contracts
{
    public interface IFranchiseRepository
    {
        // GET
        Task<PagedList<Franchise>> GetAllFranchisesAsync(FranchiseParameters franchiseParameters, bool trackChanges);
        Task<Franchise?> GetFranchiseAsync(int franchiseId, bool trackChanges);
        Task<IEnumerable<Franchise>> GetFranchisesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        void CreateFranchise(Franchise franchise);
        // DELETE
        void DeleteFranchise(Franchise franchise);
    }
}
