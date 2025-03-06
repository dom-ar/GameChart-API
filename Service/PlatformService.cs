using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Platform;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service
{
    internal sealed class PlatformService : IPlatformService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public PlatformService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<PlatformDto> platforms, MetaData metaData)> GetAllPlatformsAsync(PlatformParameters platformParameters, bool trackChanges)
        {
            var platformsWithMetaData = await _repository.Platform.GetAllPlatformsAsync(platformParameters, trackChanges);
            var platformsDto = _mapper.Map<IEnumerable<PlatformDto>>(platformsWithMetaData);

            return (platforms: platformsDto, metaData: platformsWithMetaData.MetaData);
        }

        public async Task<PlatformDto> GetPlatformAsync(int platformId, bool trackChanges)
        {
            var platform = await _repository.Platform.GetPlatformAsync(platformId, trackChanges);

            if (platform is null)
                throw new PlatformNotFoundException(platformId);

            var platformDto = _mapper.Map<PlatformDto>(platform);
            return platformDto;
        }

        public async Task<IEnumerable<PlatformDto>> GetPlatformsByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var idsList = ids.ToList();

            var platforms = await _repository.Platform.GetPlatformsByIdsAsync(idsList, trackChanges);
            if (idsList.Count() != platforms.Count())
                throw new CollectionByIdsBadRequestException();

            var platformsToReturn = _mapper.Map<IEnumerable<PlatformDto>>(platforms);
            return platformsToReturn;
        }

        public async Task<PlatformDto> CreatePlatformAsync(PlatformForCreationDto platform)
        {
            var platformEntity = _mapper.Map<Platform>(platform);

            _repository.Platform.CreatePlatform(platformEntity);
            await _repository.SaveAsync();

            var platformToReturn = _mapper.Map<PlatformDto>(platformEntity);
            return platformToReturn;
        }

        public async Task<(IEnumerable<PlatformDto> platforms, string ids)> CreatePlatformsCollectionAsync(
            IEnumerable<PlatformForCreationDto> platformsCollection)
        {
            var platforms = _mapper.Map<IEnumerable<Platform>>(platformsCollection);

            foreach (var platform in platforms)
            {
                _repository.Platform.CreatePlatform(platform);
            }
            await _repository.SaveAsync();

            var platformsToReturn = _mapper.Map<IEnumerable<PlatformDto>>(platforms).ToList();
            var ids = string.Join(", ", platformsToReturn.Select(p => p.Id));

            return (platformsToReturn, ids);
        }

        public async Task DeletePlatformAsync(int platformId, bool trackChanges)
        {
            var platform = await _repository.Platform.GetPlatformAsync(platformId, trackChanges);
            if (platform is null)
                throw new PlatformNotFoundException(platformId);

            _repository.Platform.DeletePlatform(platform);
            await _repository.SaveAsync();
        }
    }
}
