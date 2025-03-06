using AutoMapper;
using Contracts;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Dlc;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service
{
    internal sealed class DlcService : IDlcService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public DlcService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<DlcDto> dlcs, MetaData metaData)> GetDlcsAsync(int gameId, DlcParameters dlcParameters, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlcWithMetaData = await _repository.Dlc.GetDlcsAsync(gameId, dlcParameters, trackChanges);
            var dlcDto = _mapper.Map<IEnumerable<DlcDto>>(dlcWithMetaData);

            return (dlcs: dlcDto, metaData: dlcWithMetaData.MetaData);
        }

        public async Task<(IEnumerable<DlcDto> dlcs, MetaData metaData)> GetAllDlcsAsync(DlcParameters dlcParameters, bool trackChanges)
        {
            var dlcsWithMetaData = await _repository.Dlc.GetAllDlcsAsync(dlcParameters, trackChanges);
            var dlcsDto = _mapper.Map<IEnumerable<DlcDto>>(dlcsWithMetaData);

            return (dlcs: dlcsDto, metaData: dlcsWithMetaData.MetaData);
        }

        public async Task<DlcDto> GetDlcAsync(int gameId, int id, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlc = await _repository.Dlc.GetDlcAsync(gameId, id, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, id);

            var dlcDto = _mapper.Map<DlcDto>(dlc);

            return dlcDto;
        }

        public async Task<DlcDto> CreateDlcForGameAsync(int gameId, DlcForCreationDto dlcForCreation, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlcEntity = _mapper.Map<Dlc>(dlcForCreation);

            _repository.Dlc.CreateDlcForGame(gameId, dlcEntity);
            await _repository.SaveAsync();

            var dlcToReturn = _mapper.Map<DlcDto>(dlcEntity);

            return dlcToReturn;
        }

        public async Task<(IEnumerable<DlcDto> dlcs, string ids)> CreateDlcCollectionForGameAsync(int gameId,
            IEnumerable<DlcForCreationDto> dlcsCollection, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlcsEntities = _mapper.Map<IEnumerable<Dlc>>(dlcsCollection);

            foreach (var dlc in dlcsEntities)
            {
                _repository.Dlc.CreateDlcForGame(gameId, dlc);
            }

            await _repository.SaveAsync();

            var dlcsToReturn = _mapper.Map<IEnumerable<DlcDto>>(dlcsEntities).ToList();
            var ids = string.Join(", ", dlcsToReturn.Select(d => d.Id));

            return (dlcsToReturn, ids);


        }

        public async Task UpdateDlcAsync(int gameId, int id, DlcForUpdateDto dlcForUpdate, bool gameTrackChanges,
            bool dlcTrackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, gameTrackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlc = await _repository.Dlc.GetDlcAsync(gameId, id, dlcTrackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, id);

            _mapper.Map(dlcForUpdate, dlc);
            await _repository.SaveAsync();
        }

        public async Task DeleteDlcFromGameAsync(int gameId, int dlcId, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, dlcId);

            _repository.Dlc.DeleteDlcFromGame(gameId, dlc);
            await _repository.SaveAsync();
        }
    }
}