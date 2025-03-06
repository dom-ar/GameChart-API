using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Repository
{
    public class PlatformRepository : RepositoryBase<Platform>, IPlatformRepository
    {
        public PlatformRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Platform>> GetAllPlatformsAsync(PlatformParameters platformParameters,
            bool trackChanges)
        {
            var platforms = await FindAll(trackChanges)
                .OrderBy(p => p.Name)
                .Skip((platformParameters.PageNumber - 1) * platformParameters.PageSize)
                .Take(platformParameters.PageSize)
                .ToListAsync();

            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<Platform>(platforms, count, platformParameters.PageNumber,
                platformParameters.PageSize);
        }
            
        public async Task<Platform?> GetPlatformAsync(int platformId, bool trackChanges) =>
            await FindByCondition(p => p.Id.Equals(platformId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Platform>> GetPlatformsByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(p => ids.Contains(p.Id), trackChanges)
                .ToListAsync();

        public void CreatePlatform(Platform platform) => Create(platform);

        public void DeletePlatform(Platform platform) => Delete(platform);
    }
}