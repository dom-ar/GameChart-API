using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Genre;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service
{
    internal sealed class GenreService : IGenreService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public GenreService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<GenreDto> genres, MetaData metaData)> GetAllGenresAsync(GenreParameters genreParameters, bool trackChanges)
        {
            var genresWithMetaData = await _repository.Genre.GetAllGenresAsync(genreParameters, trackChanges);
            var genresDto = _mapper.Map<IEnumerable<GenreDto>>(genresWithMetaData);

            return (genres: genresDto, metaData: genresWithMetaData.MetaData);
        }

        public async Task<GenreDto> GetGenreAsync(int genreId, bool trackChanges)
        {
            var genre = await _repository.Genre.GetGenreAsync(genreId, trackChanges);

            if (genre is null)
                throw new GenreNotFoundException(genreId);

            var genreDto = _mapper.Map<GenreDto>(genre);
            return genreDto;
        }

        public async Task<IEnumerable<GenreDto>> GetGenresByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var idsList = ids.ToList();

            var genres = await _repository.Genre.GetGenresByIdsAsync(idsList, trackChanges);
            if (idsList.Count() != genres.Count())
                throw new CollectionByIdsBadRequestException();

            var genresToReturn = _mapper.Map<IEnumerable<GenreDto>>(genres);
            return genresToReturn;
        }

        public async Task<GenreDto> CreateGenreAsync(GenreForCreationDto genre)
        {
            var genreEntity = _mapper.Map<Genre>(genre);

            _repository.Genre.CreateGenre(genreEntity);
            await _repository.SaveAsync();

            var genreToReturn = _mapper.Map<GenreDto>(genreEntity);
            return genreToReturn;
        }

        public async Task<(IEnumerable<GenreDto> genres, string ids)> CreateGenresCollectionAsync(
            IEnumerable<GenreForCreationDto> genresCollection)
        {
            var genres = _mapper.Map<IEnumerable<Genre>>(genresCollection);

            foreach (var genre in genres)
            {
                _repository.Genre.CreateGenre(genre);
            }
            await _repository.SaveAsync();

            var genresToReturn = _mapper.Map<IEnumerable<GenreDto>>(genres).ToList();
            var ids = string.Join(", ", genresToReturn.Select(g => g.Id));

            return (genres: genresToReturn, ids: ids);
        }

        public async Task DeleteGenreAsync(int genreId, bool trackChanges)
        {
            var genre = await _repository.Genre.GetGenreAsync(genreId, trackChanges);
            if (genre is null)
                throw new GenreNotFoundException(genreId);

            _repository.Genre.DeleteGenre(genre);
            await _repository.SaveAsync();
        }
    }
}
