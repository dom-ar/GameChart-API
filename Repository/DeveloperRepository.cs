using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Repository
{
    internal sealed class DeveloperRepository : RepositoryBase<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        // GET

        public async Task<PagedList<Developer>> GetAllDevelopersAsync(DeveloperParameters developerParameters,
            bool trackChanges)
        {
            var developers = await FindAll(trackChanges)
                .OrderBy(d => d.Id)
                // For large table (skip and take)
                .Skip((developerParameters.PageNumber - 1) * developerParameters.PageSize)
                .Take(developerParameters.PageSize)
                .ToListAsync();

            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<Developer>(developers, count, developerParameters.PageNumber,
                developerParameters.PageSize);

            //return PagedList<Developer>
            //    .ToPagedList(developers, developerParameters.PageNumber, developerParameters.PageSize);
        }

        public async Task<Developer?> GetDeveloperAsync(int developerId, bool trackChanges) =>
            await FindByCondition(d => d.Id.Equals(developerId), trackChanges).SingleOrDefaultAsync();

        public async Task<IEnumerable<Developer>> GetDevelopersByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(d => ids.Contains(d.Id), trackChanges)
            .ToListAsync();

        // POST
        public void CreateDeveloper(Developer developer) => Create(developer);

        // DELETE
        public void DeleteDeveloper(Developer developer) => Delete(developer);
    }
}
