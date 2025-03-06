namespace Entities.Exceptions.NotFound
{
    public sealed class GameDeveloperNotFoundException : NotFoundException
    {
        public GameDeveloperNotFoundException(int gameId, int developerId)
            : base($"The developer with id: {developerId} is not part of the game with id: {gameId}.")
        {
        }
    }
}