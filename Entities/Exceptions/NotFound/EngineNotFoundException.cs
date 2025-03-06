namespace Entities.Exceptions.NotFound
{
    public sealed class EngineNotFoundException : NotFoundException
    {
        public EngineNotFoundException(int engineId)
            : base($"Engine with id: {engineId} doesn't exist in the database.") { }

        public EngineNotFoundException(IEnumerable<int> engineIds)
            : base($"Engines with ids: {string.Join(", ", engineIds)} don't exist in the database.") { }
    }
}