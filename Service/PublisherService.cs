using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Publisher;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service
{
    internal sealed class PublisherService : IPublisherService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public PublisherService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<PublisherDto> publishers, MetaData metaData)> GetAllPublishersAsync(PublisherParameters publisherParameters, bool trackChanges)
        {
            var publishersWithMetaData = await _repository.Publisher.GetAllPublishersAsync(publisherParameters, trackChanges);
            var publishersDto = _mapper.Map<IEnumerable<PublisherDto>>(publishersWithMetaData);

            return (publishers: publishersDto, metaData: publishersWithMetaData.MetaData);
        }

        public async Task<PublisherDto> GetPublisherAsync(int publisherId, bool trackChanges)
        {
            var publisher = await _repository.Publisher.GetPublisherAsync(publisherId, trackChanges);

            if (publisher is null)
                throw new PublisherNotFoundException(publisherId);

            var publisherDto = _mapper.Map<PublisherDto>(publisher);
            return publisherDto;
        }

        public async Task<IEnumerable<PublisherDto>> GetPublishersByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var idsList = ids.ToList();

            var publishers = await _repository.Publisher.GetPublishersByIdsAsync(idsList, trackChanges);
            if (idsList.Count() != publishers.Count())
                throw new CollectionByIdsBadRequestException();

            var publishersToReturn = _mapper.Map<IEnumerable<PublisherDto>>(publishers);
            return publishersToReturn;
        }

        public async Task<PublisherDto> CreatePublisherAsync(PublisherForCreationDto publisher)
        {
            var publisherEntity = _mapper.Map<Publisher>(publisher);

            _repository.Publisher.CreatePublisher(publisherEntity);
            await _repository.SaveAsync();

            var publisherToReturn = _mapper.Map<PublisherDto>(publisherEntity);
            return publisherToReturn;
        }

        public async Task<(IEnumerable<PublisherDto> publishers, string ids)> CreatePublishersCollectionAsync(
            IEnumerable<PublisherForCreationDto> publishersCollection)
        {
            var publishers = _mapper.Map<IEnumerable<Publisher>>(publishersCollection);

            foreach (var publisher in publishers)
            {
                _repository.Publisher.CreatePublisher(publisher);
            }
            await _repository.SaveAsync();

            var publishersToReturn = _mapper.Map<IEnumerable<PublisherDto>>(publishers).ToList();
            var ids = string.Join(",", publishersToReturn.Select(p => p.Id));

            return (publishers: publishersToReturn, ids: ids);
        }

        public async Task DeletePublisherAsync(int publisherId, bool trackChanges)
        {
            var publisherEntity = await _repository.Publisher.GetPublisherAsync(publisherId, trackChanges);
            if (publisherEntity is null)
                throw new PublisherNotFoundException(publisherId);

            _repository.Publisher.DeletePublisher(publisherEntity);
            await _repository.SaveAsync();
        }
    }
}
