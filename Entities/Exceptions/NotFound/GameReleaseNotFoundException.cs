namespace Entities.Exceptions.NotFound
{
    public sealed class GameReleaseNotFoundException : NotFoundException
    {
        public GameReleaseNotFoundException(int gameReleaseId)
            : base($"GameRelease with id: {gameReleaseId} doesn't exist in the database.") { }
    }
}