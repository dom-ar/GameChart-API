using Shared.DataTransferObjects.Publisher;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service.Contracts
{
    public interface IPublisherService
    {
        // GET
        Task<(IEnumerable<PublisherDto> publishers, MetaData metaData)> GetAllPublishersAsync(PublisherParameters publisherParameters, bool trackChanges);
        Task<PublisherDto> GetPublisherAsync(int publisherId, bool trackChanges);
        Task<IEnumerable<PublisherDto>> GetPublishersByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        Task<PublisherDto> CreatePublisherAsync(PublisherForCreationDto publisher);
        Task<(IEnumerable<PublisherDto> publishers, string ids)> CreatePublishersCollectionAsync(
            IEnumerable<PublisherForCreationDto> publishersCollection);
        // DELETE
        Task DeletePublisherAsync(int publisherId, bool trackChanges);
    }
}
