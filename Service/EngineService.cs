using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Engine;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service
{
    internal sealed class EngineService : IEngineService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EngineService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<EngineDto> engines, MetaData metaData)> GetAllEnginesAsync(EngineParameters engineParameters, bool trackChanges)
        {
            var enginesWithMetaData = await _repository.Engine.GetAllEnginesAsync(engineParameters, trackChanges);
            var enginesDto = _mapper.Map<IEnumerable<EngineDto>>(enginesWithMetaData);

            return (engines: enginesDto, metaData: enginesWithMetaData.MetaData);
        }

        public async Task<EngineDto> GetEngineAsync(int engineId, bool trackChanges)
        {
            var engine = await _repository.Engine.GetEngineAsync(engineId, trackChanges);

            if (engine is null)
                throw new EngineNotFoundException(engineId);

            var engineDto = _mapper.Map<EngineDto>(engine);
            return engineDto;
        }
        
        public async Task<IEnumerable<EngineDto>> GetEnginesByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var idsList = ids.ToList();

            var engines = await _repository.Engine.GetEnginesByIdsAsync(idsList, trackChanges);
            if (idsList.Count() != engines.Count())
                throw new CollectionByIdsBadRequestException();

            var enginesToReturn = _mapper.Map<IEnumerable<EngineDto>>(engines);
            return enginesToReturn;
        }

        public async Task<EngineDto> CreateEngineAsync(EngineForCreationDto engine)
        {
            var engineEntity = _mapper.Map<Engine>(engine);

            _repository.Engine.CreateEngine(engineEntity);
            await _repository.SaveAsync();

            var engineToReturn = _mapper.Map<EngineDto>(engineEntity);
            return engineToReturn;
        }

        public async Task<(IEnumerable<EngineDto> engines, string ids)> CreateEnginesCollectionAsync(IEnumerable<EngineForCreationDto> enginesCollection)
        {
            var engines = _mapper.Map<IEnumerable<Engine>>(enginesCollection);

            foreach (var engine in engines)
            {
                _repository.Engine.CreateEngine(engine);
            }
            await _repository.SaveAsync();

            var enginesToReturn = _mapper.Map<IEnumerable<EngineDto>>(engines).ToList();
            var ids = string.Join(", ", enginesToReturn.Select(e => e.Id));

            return (engines: enginesToReturn, ids: ids);
        }

        public  async Task DeleteEngineAsync(int engineId, bool trackChanges)
        {
            var engine = await _repository.Engine.GetEngineAsync(engineId, trackChanges);
            if (engine is null)
                throw new EngineNotFoundException(engineId);

            _repository.Engine.DeleteEngine(engine);
            await _repository.SaveAsync();
        }
    }
}