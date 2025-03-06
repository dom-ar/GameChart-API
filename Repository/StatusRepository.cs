using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class StatusRepository : RepositoryBase<Status>, IStatusRepository
    {
        public StatusRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Status>> GetAllStatusAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(s => s.Name)
                .ToListAsync();

        public async Task<Status> GetStatusAsync(int statusId, bool trackChanges) =>
            await FindByCondition(s => s.Id.Equals(statusId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Status>> GetStatusesByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(s => ids.Contains(s.Id), trackChanges)
                .ToListAsync();

        public void CreateStatus(Status status) => Create(status);

        public void DeleteStatus(Status status) => Delete(status);
    }
}