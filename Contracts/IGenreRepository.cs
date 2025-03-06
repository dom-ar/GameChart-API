using Entities.Models;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Contracts
{
    public interface IGenreRepository
    {
        // GET
        Task<PagedList<Genre>> GetAllGenresAsync(GenreParameters genreParameters, bool trackChanges);
        Task<Genre?> GetGenreAsync(int genreId, bool trackChanges);
        Task<IEnumerable<Genre>> GetGenresByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        // POST
        void CreateGenre(Genre genre);
        // DELETE
        void DeleteGenre(Genre genre);
    }
}
