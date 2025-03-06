using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Repository
{
    public class FranchiseRepository : RepositoryBase<Franchise>, IFranchiseRepository
    {
        public FranchiseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Franchise>> GetAllFranchisesAsync(FranchiseParameters franchiseParameters,
            bool trackChanges)
        {
            var franchises = await FindAll(trackChanges)
                .OrderBy(f => f.Name)
                .Skip((franchiseParameters.PageNumber - 1) * franchiseParameters.PageSize)
                .Take(franchiseParameters.PageSize)
                .ToListAsync();

            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<Franchise>(franchises, count, franchiseParameters.PageNumber,
                franchiseParameters.PageSize);
        }

        public async Task<Franchise?> GetFranchiseAsync(int franchiseId, bool trackChanges) =>
            await FindByCondition(f => f.Id.Equals(franchiseId), trackChanges).SingleOrDefaultAsync();

        public async Task<IEnumerable<Franchise>> GetFranchisesByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(f => ids.Contains(f.Id), trackChanges)
                .ToListAsync();

        public void CreateFranchise(Franchise franchise) => Create(franchise);

        public void DeleteFranchise(Franchise franchise) => Delete(franchise);
    }
}