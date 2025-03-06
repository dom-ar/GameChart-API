using Shared.DataTransferObjects.Genre;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Service.Contracts
{
    public interface IGenreService
    {
        // GET
        Task<(IEnumerable<GenreDto> genres, MetaData metaData)> GetAllGenresAsync(GenreParameters genreParameters, bool trackChanges);
        Task<GenreDto> GetGenreAsync(int genreId, bool trackChanges);
        Task<IEnumerable<GenreDto>> GetGenresByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        Task<GenreDto> CreateGenreAsync(GenreForCreationDto genre);
        Task<(IEnumerable<GenreDto> genres, string ids)> CreateGenresCollectionAsync(IEnumerable<GenreForCreationDto> genresCollection);
        // DELETE
        Task DeleteGenreAsync(int genreId, bool trackChanges);
    }
}
