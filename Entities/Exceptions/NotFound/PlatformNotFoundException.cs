namespace Entities.Exceptions.NotFound
{
    public sealed class PlatformNotFoundException : NotFoundException
    {
        public PlatformNotFoundException(int platformId)
            : base($"Platform with id: {platformId} doesn't exist in the database.") { }
    }
}