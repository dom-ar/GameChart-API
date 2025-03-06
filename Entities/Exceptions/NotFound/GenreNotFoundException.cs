namespace Entities.Exceptions.NotFound
{
    public sealed class GenreNotFoundException : NotFoundException
    {
        public GenreNotFoundException(int genreId)
            : base($"Genre with id: {genreId} doesn't exist in the database.") { }

        public GenreNotFoundException(IEnumerable<int> genreIds)
            : base($"Genres with ids: {string.Join(", ", genreIds)} don't exist in the database.") { }
    }
}