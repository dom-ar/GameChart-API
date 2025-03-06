using Entities.Models;

namespace Contracts
{
    public interface IStatusRepository
    {
        // GET
        Task<IEnumerable<Status>> GetAllStatusAsync(bool trackChanges);
        Task<Status> GetStatusAsync(int statusId, bool trackChanges);
        Task<IEnumerable<Status>> GetStatusesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        void CreateStatus(Status status);
        // DELETE
        void DeleteStatus(Status status);
    }
}
