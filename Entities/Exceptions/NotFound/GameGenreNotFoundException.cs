namespace Entities.Exceptions.NotFound
{
    public sealed class GameGenreNotFoundException : NotFoundException
    {
        public GameGenreNotFoundException(int gameId, int genreId)
            : base($"The genre with id: {genreId} is not part of the game with id: {gameId}.")
        {
        }
    }
}