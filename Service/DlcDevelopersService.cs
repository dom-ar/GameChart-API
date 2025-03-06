using AutoMapper;
using Contracts;
using Entities.Exceptions.NotFound;
using Entities.Models.Joint_Models;
using Service.Contracts;
using Shared.DataTransferObjects.DlcDevelopers;

namespace Service
{
    internal sealed class DlcDevelopersService : IDlcDevelopersService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public DlcDevelopersService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DlcDevelopersDto>> GetDlcDevelopersAsync(int gameId, int dlcId, bool trackChanges)
        {
            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId,dlcId);

            var dlcDevelopers = await _repository.DlcDevelopers.GetDlcDevelopersAsync(dlcId, trackChanges);
            var dlcDevelopersDto = _mapper.Map<IEnumerable<DlcDevelopersDto>>(dlcDevelopers);

            return dlcDevelopersDto;
        }

        public async Task<DlcDevelopersDto> AddDeveloperToDlcAsync(int gameId, int dlcId, DlcDevelopersForCreationDto dlcDevelopers, bool trackChanges)
        {
            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId,dlcId);

            var developer = await _repository.Developer.GetDeveloperAsync(dlcDevelopers.DeveloperId, trackChanges);
            if (developer is null)
                throw new DeveloperNotFoundException(dlcDevelopers.DeveloperId);

            var dlcDevelopersEntity = _mapper.Map<DlcDevelopers>(dlcDevelopers);

            _repository.DlcDevelopers.AddDeveloperToDlc(dlcId, dlcDevelopersEntity);
            await _repository.SaveAsync();

            var dlcDevelopersToReturn = _mapper.Map<DlcDevelopersDto>(dlcDevelopersEntity);
            return dlcDevelopersToReturn;
        }

        public async Task<(IEnumerable<DlcDevelopersDto> dlcDevelopers, string ids)> AddDlcDevelopersCollectionAsync(int gameId,
            int dlcId, IEnumerable<DlcDevelopersForCreationDto> dlcDevelopersCollection, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, dlcId);

            var dlcDevelopersCollectionList = dlcDevelopersCollection.ToList();
            var dlcDevelopersEntities = _mapper.Map<IEnumerable<DlcDevelopers>>(dlcDevelopersCollectionList);

            foreach (var developer in dlcDevelopersCollectionList)
            {
                var developerEntity = await _repository.Developer.GetDeveloperAsync(developer.DeveloperId, trackChanges);
                if (developerEntity is null)
                    throw new DeveloperNotFoundException(developer.DeveloperId);
            }

            foreach (var dlcDeveloper in dlcDevelopersEntities)
            {
                _repository.DlcDevelopers.AddDeveloperToDlc(dlcId, dlcDeveloper);
            }

            await _repository.SaveAsync();

            var dlcDevelopersToReturn = _mapper.Map<IEnumerable<DlcDevelopersDto>>(dlcDevelopersEntities).ToList();
            var ids = string.Join(",", dlcDevelopersToReturn.Select(d => d.DeveloperId));

            return (dlcDevelopers: dlcDevelopersToReturn, ids: ids);
        }

        public async Task RemoveDeveloperFromDlcAsync(int gameId, int dlcId, int developerId, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, dlcId);

            var developer = await _repository.Developer.GetDeveloperAsync(developerId, trackChanges);
            if (developer is null)
                throw new DeveloperNotFoundException(developerId);

            //var dlcDeveloperEntity = await _repository.DlcDevelopers.GetDlcDevelopersAsync(dlcId, trackChanges)
            //    .FirstOrDefault(dd => dd.DeveloperId == developerId);
            //if (dlcDeveloperEntity is null)
            //    throw new DlcDeveloperNotFoundException(dlcId, developerId);

            //_repository.DlcDevelopers.RemoveDeveloperFromDlc(dlcDeveloperEntity);
            //await _repository.SaveAsync();
        }

    }
}