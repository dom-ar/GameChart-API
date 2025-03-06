using Entities.Models;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Contracts
{
    public interface IEngineRepository
    {
        // GET
        Task<PagedList<Engine>> GetAllEnginesAsync(EngineParameters engineParameters, bool trackChanges);
        Task<Engine?> GetEngineAsync(int engineId, bool trackChanges);
        Task<IEnumerable<Engine>> GetEnginesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        void CreateEngine(Engine engine);
        // DELETE
        void DeleteEngine(Engine engine);
    }
}
