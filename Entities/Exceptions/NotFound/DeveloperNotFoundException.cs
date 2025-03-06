namespace Entities.Exceptions.NotFound
{
    public sealed class DeveloperNotFoundException : NotFoundException
    {
        public DeveloperNotFoundException(int developerId)
            : base($"The developer with id: {developerId} doesn't exist in the database.") { }

        public DeveloperNotFoundException(IEnumerable<int> developerIds)
            : base($"The developers with ids: {string.Join(", ", developerIds)} don't exist in the database.") { }
    }
}
