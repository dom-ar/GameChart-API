using Entities.Models;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Contracts
{
    public interface IPublisherRepository
    {
        // GET
        Task<PagedList<Publisher>> GetAllPublishersAsync(PublisherParameters publisherParameters, bool trackChanges);
        Task<Publisher?> GetPublisherAsync(int publisherId, bool trackChanges);
        Task<IEnumerable<Publisher>> GetPublishersByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        void CreatePublisher(Publisher publisher);
        // DELETE 
        void DeletePublisher(Publisher publisher);
    }
}
