using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Repository
{
    public class PublisherRepository : RepositoryBase<Publisher>, IPublisherRepository
    {
        public PublisherRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        // GET

        public async Task<PagedList<Publisher>> GetAllPublishersAsync(PublisherParameters publisherParameters,
            bool trackChanges)
        {
            var publishers = await FindAll(trackChanges)
                .OrderBy(p => p.Name)
                .Skip((publisherParameters.PageNumber - 1) * publisherParameters.PageSize)
                .Take(publisherParameters.PageSize)
                .ToListAsync();

            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<Publisher>(publishers, count, publisherParameters.PageNumber,
                publisherParameters.PageSize);
        }
        

        public async Task<Publisher?> GetPublisherAsync(int publisherId, bool trackChanges) =>
            await FindByCondition(p => p.Id.Equals(publisherId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Publisher>> GetPublishersByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(p => ids.Contains(p.Id), trackChanges)
                .ToListAsync();

        // POST
        public void CreatePublisher(Publisher publisher) => Create(publisher);

        // DELETE
        public void DeletePublisher(Publisher publisher) => Delete(publisher);
    }
}