using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Repository
{
    public class EngineRepository : RepositoryBase<Engine>, IEngineRepository
    {
        public EngineRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Engine>> GetAllEnginesAsync(EngineParameters engineParameters, bool trackChanges)
        {
            var engines = await FindAll(trackChanges)
                .OrderBy(e => e.Name)
                .Skip((engineParameters.PageNumber - 1) * engineParameters.PageSize)
                .Take(engineParameters.PageSize)
                .ToListAsync();

            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<Engine>(engines, count, engineParameters.PageNumber, engineParameters.PageSize);
        }
            

        public async Task<Engine?> GetEngineAsync(int engineId, bool trackChanges) =>
            await FindByCondition(e => e.Id.Equals(engineId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Engine>> GetEnginesByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(e => ids.Contains(e.Id), trackChanges)
                .ToListAsync();

        public void CreateEngine(Engine engine) => Create(engine);

        public void DeleteEngine(Engine engine) => Delete(engine);
    }
}