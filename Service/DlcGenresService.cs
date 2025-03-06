using AutoMapper;
using Contracts;
using Entities.Exceptions.NotFound;
using Entities.Models.Joint_Models;
using Service.Contracts;
using Shared.DataTransferObjects.DlcGenres;

namespace Service
{
    internal sealed class DlcGenresService : IDlcGenresService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public DlcGenresService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DlcGenresDto>> GetDlcGenresAsync(int gameId, int dlcId, bool trackChanges)
        {
            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId,dlcId);

            var dlcGenres = await _repository.DlcGenres.GetDlcGenresAsync(dlcId, trackChanges);
            var dlcGenresDto = _mapper.Map<IEnumerable<DlcGenresDto>>(dlcGenres);

            return dlcGenresDto;
        }

        public async Task<DlcGenresDto> AddGenreToDlcAsync(int gameId, int dlcId, DlcGenresForCreationDto dlcGenres, bool trackChanges)
        {
            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, dlcId);

            var genre = await _repository.Genre.GetGenreAsync(dlcGenres.GenreId, trackChanges);
            if (genre is null)
                throw new GenreNotFoundException(dlcGenres.GenreId);

            var dlcGenresEntity = _mapper.Map<DlcGenres>(dlcGenres);

            _repository.DlcGenres.AddGenreToDlc(dlcId, dlcGenresEntity);
            await _repository.SaveAsync();

            var dlcGenresToReturn = _mapper.Map<DlcGenresDto>(dlcGenresEntity);
            return dlcGenresToReturn;
        }

        public async Task<(IEnumerable<DlcGenresDto> dlcGenres, string ids)> AddDlcGenresCollectionAsync(int gameId, int dlcId,
            IEnumerable<DlcGenresForCreationDto> dlcGenresCollection, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, dlcId);

            var dlcGenresCollectionList = dlcGenresCollection.ToList();
            var dlcGenresEntities = _mapper.Map<IEnumerable<DlcGenres>>(dlcGenresCollectionList);

            foreach (var genre in dlcGenresCollectionList)
            {
                var genreEntity = await _repository.Genre.GetGenreAsync(genre.GenreId, trackChanges);
                if (genreEntity is null)
                    throw new GenreNotFoundException(genre.GenreId);
            }

            foreach (var dlcGenresEntity in dlcGenresEntities)
            {
                _repository.DlcGenres.AddGenreToDlc(dlcId, dlcGenresEntity);
            }

            await _repository.SaveAsync();

            var dlcGenresToReturn = _mapper.Map<IEnumerable<DlcGenresDto>>(dlcGenresEntities).ToList();
            var ids = string.Join(",", dlcGenresToReturn.Select(dg => dg.GenreId));

            return (dlcGenres: dlcGenresToReturn, ids: ids);
        }

        public async Task RemoveGenreFromDlcAsync(int gameId, int dlcId, int genreId, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameId, trackChanges);
            if (game is null)
                throw new GameNotFoundException(gameId);

            var dlc = await _repository.Dlc.GetDlcAsync(gameId, dlcId, trackChanges);
            if (dlc is null)
                throw new DlcNotFoundException(gameId, dlcId);

            var genre = await _repository.Genre.GetGenreAsync(genreId, trackChanges);
            if (genre is null)
                throw new GenreNotFoundException(genreId);

            //var dlcGenreEntity = await _repository.DlcGenres.GetDlcGenresAsync(dlcId, trackChanges)
            //    .FirstOrDefault(dg => dg.GenreId == genreId);
            //if (dlcGenreEntity is null)
            //    throw new DlcGenreNotFoundException(dlcId, genreId);

            //_repository.DlcGenres.RemoveGenreFromDlc(dlcGenreEntity);
            //await _repository.SaveAsync();
        }

    }
}