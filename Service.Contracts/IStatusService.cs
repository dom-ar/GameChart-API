using Shared.DataTransferObjects.Status;

namespace Service.Contracts
{
    public interface IStatusService
    {
        // GET
        Task<IEnumerable<StatusDto>> GetAllStatusAsync(bool trackChanges);
        Task<StatusDto> GetStatusAsync(int statusId, bool trackChanges);
        Task<IEnumerable<StatusDto>> GetStatusesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        Task<StatusDto> CreateStatusAsync(StatusForCreationDto status);
        Task<(IEnumerable<StatusDto> statuses, string ids)> CreateStatusesCollectionAsync(IEnumerable<StatusForCreationDto> statuses);
        // DELETE
        Task DeleteStatusAsync(int statusId, bool trackChanges);
    }
}
