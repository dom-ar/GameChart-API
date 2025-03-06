using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntityParameters;

namespace Repository
{
    internal sealed class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public async Task<PagedList<Genre>> GetAllGenresAsync(GenreParameters genreParameters, bool trackChanges)
        {
            var genres = await FindAll(trackChanges)
                .OrderBy(d => d.Name)
                .Skip((genreParameters.PageNumber - 1) * genreParameters.PageSize)
                .Take(genreParameters.PageSize)
                .ToListAsync();

            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<Genre>(genres, count, genreParameters.PageNumber, genreParameters.PageSize);
        }
            

        public async Task<Genre?> GetGenreAsync(int genreId, bool trackChanges) =>
            await FindByCondition(d => d.Id.Equals(genreId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Genre>> GetGenresByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(d => ids.Contains(d.Id), trackChanges)
                .ToListAsync();

        public void CreateGenre(Genre genre) => Create(genre);

        public void DeleteGenre(Genre genre) => Delete(genre);
    }
}