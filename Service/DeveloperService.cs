using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Developer;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service
{
    internal sealed class DeveloperService : IDeveloperService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public DeveloperService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET

        // Get all developers
        public async Task<(IEnumerable<DeveloperDto> developers, MetaData metaData)> GetAllDevelopersAsync(DeveloperParameters developerParameters, bool trackChanges)
        {
            var developersWithMetaData =
                await _repository.Developer.GetAllDevelopersAsync(developerParameters, trackChanges);
            var developersDto = _mapper.Map<IEnumerable<DeveloperDto>>(developersWithMetaData);

            return (developers: developersDto, metaData: developersWithMetaData.MetaData);
        }

        // Get a single developer by id
        public async Task<DeveloperDto> GetDeveloperAsync(int developerId, bool trackChanges)
        {
            var developer = await _repository.Developer.GetDeveloperAsync(developerId, trackChanges);
            if (developer is null )
                throw new DeveloperNotFoundException(developerId);

            var developerDto = _mapper.Map<DeveloperDto>(developer);
            return developerDto;
        }

        // Get a collection of developers by ids
        public async Task<IEnumerable<DeveloperDto>> GetDevelopersByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var idsList = ids.ToList();

            var developers = await _repository.Developer.GetDevelopersByIdsAsync(idsList, trackChanges);
            if (idsList.Count() != developers.Count())
                throw new CollectionByIdsBadRequestException();

            var developersToReturn = _mapper.Map<IEnumerable<DeveloperDto>>(developers);

            return developersToReturn;
        }

        // POST

        // Create a developer
        public async Task<DeveloperDto> CreateDeveloperAsync(DeveloperForCreationDto developer)
        {
            var developerEntity = _mapper.Map<Developer>(developer);

            _repository.Developer.CreateDeveloper(developerEntity);
            await _repository.SaveAsync();

            var developerToReturn = _mapper.Map<DeveloperDto>(developerEntity);
            return developerToReturn;
        }

        // Create a collection of developers
        public async Task<(IEnumerable<DeveloperDto> developers, string ids)> CreateDevelopersCollectionAsync(IEnumerable<DeveloperForCreationDto>? developersCollection)
        {
            var developersCollectionList = developersCollection.ToList();
            var developers = _mapper.Map<List<Developer>>(developersCollection);

            foreach (var developer in developers)
            {
                _repository.Developer.CreateDeveloper(developer);
            }
            await _repository.SaveAsync();

            var developersCollectionToReturn = _mapper.Map<IEnumerable<DeveloperDto>>(developers).ToList();
            var ids = string.Join(",", developersCollectionToReturn.Select(d => d.Id));

            return (developers: developersCollectionToReturn, ids: ids);
        }

        // DELETE

        // Delete a developer
        public async Task DeleteDeveloperAsync(int developerId, bool trackChanges)
        {
            var developer = await _repository.Developer.GetDeveloperAsync(developerId, trackChanges);
            if (developer is null)
                throw new DeveloperNotFoundException(developerId);

            _repository.Developer.DeleteDeveloper(developer);
            await _repository.SaveAsync();
        }


    }
}
