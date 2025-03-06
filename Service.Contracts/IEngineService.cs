using Shared.DataTransferObjects.Engine;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service.Contracts
{
    public interface IEngineService
    {
        // GET
        Task<(IEnumerable<EngineDto> engines, MetaData metaData)> GetAllEnginesAsync(EngineParameters engineParameters, bool trackChanges);
        Task<EngineDto> GetEngineAsync(int engineId, bool trackChanges);
        Task<IEnumerable<EngineDto>> GetEnginesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        Task<EngineDto> CreateEngineAsync(EngineForCreationDto engine);
        Task<(IEnumerable<EngineDto> engines, string ids)> CreateEnginesCollectionAsync(IEnumerable<EngineForCreationDto> enginesCollection);
        // DELETE
        Task DeleteEngineAsync(int engineId, bool trackChanges);
    }
}
