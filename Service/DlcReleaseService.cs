using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.DlcRelease;

namespace Service
{
    internal sealed class DlcReleaseService : IDlcReleaseService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public DlcReleaseService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DlcReleaseDto>> GetDlcReleasesAsync(int gameId, int dlcId, bool trackChanges)
        {
            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId,dlcId);

            var dlcReleases = await _repository.DlcRelease.GetDlcReleasesAsync(dlcId, trackChanges);
            var dlcReleasesDto = _mapper.Map<IEnumerable<DlcReleaseDto>>(dlcReleases);

            return dlcReleasesDto;
        }

        public async Task<IEnumerable<DlcReleaseDto>> GetDlcReleasesByIdsAsync(int gameId, int dlcId, IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var idsList = ids.ToList();

            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, dlcId);

            var dlcReleases = await _repository.DlcRelease.GetDlcReleasesByIdsAsync(dlcId, idsList, trackChanges);
            if (dlcReleases.Count() != idsList.Count)
                throw new CollectionByIdsBadRequestException();

            var dlcReleasesToReturn = _mapper.Map<IEnumerable<DlcReleaseDto>>(dlcReleases);
            return dlcReleasesToReturn;
        }

        public async Task<DlcReleaseDto> GetDlcReleaseAsync(int gameId, int dlcId, int id, bool trackChanges)
        {
            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, dlcId);

            var dlcRelease = await _repository.DlcRelease.GetDlcReleaseAsync(dlcId, id, trackChanges);
            if (dlcRelease is null)
                throw new DlcReleaseNotFoundException(id);

            var dlcReleaseDto = _mapper.Map<DlcReleaseDto>(dlcRelease);
            return dlcReleaseDto;
        }

        public async Task<DlcReleaseDto> CreateReleaseForDlcAsync(int gameId, int dlcId, DlcReleaseCreationDto dlcReleaseCreation,
            bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, dlcId);

            var dlcRelease = _mapper.Map<DlcRelease>(dlcReleaseCreation);

            _repository.DlcRelease.CreateReleaseForDlc(dlcId, dlcRelease);
            await _repository.SaveAsync();

            var dlcReleaseDto = _mapper.Map<DlcReleaseDto>(dlcRelease);
            return dlcReleaseDto;
        }

        public async Task<(IEnumerable<DlcReleaseDto> dlcReleases, string ids)> CreateDlcReleasesCollectionAsync(int gameId, int dlcId,
            IEnumerable<DlcReleaseCreationDto> dlcReleasesCollection, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, dlcId);

            var dlcReleasesEntities = _mapper.Map<IEnumerable<DlcRelease>>(dlcReleasesCollection);

            foreach (var dlcRelease in dlcReleasesEntities)
            {
                _repository.DlcRelease.CreateReleaseForDlc(dlcId, dlcRelease);
            }
            await _repository.SaveAsync();

            var dlcReleasesToReturn = _mapper.Map<IEnumerable<DlcReleaseDto>>(dlcReleasesEntities).ToList();
            var ids = string.Join(",", dlcReleasesToReturn.Select(x => x.Id));

            return (dlcReleasesToReturn, ids);
        }

        public async Task DeleteReleaseFromDlcAsync(int gameId, int dlcId, int id, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, dlcId);

            var dlcReleaseEntity = await _repository.DlcRelease.GetDlcReleaseAsync(dlcId, id, trackChanges);
            if (dlcReleaseEntity is null)
                throw new DlcReleaseNotFoundException(dlcId);

            _repository.DlcRelease.DeleteReleaseFromDlc(dlcReleaseEntity);
            await _repository.SaveAsync();
        }
    }
}
