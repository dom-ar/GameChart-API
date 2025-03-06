namespace Entities.Exceptions.NotFound
{
    public sealed class PublisherNotFoundException : NotFoundException
    {
        public PublisherNotFoundException(int publisherId)
            : base($"Publisher with id: {publisherId} doesn't exist in the database.") { }

        public PublisherNotFoundException(IEnumerable<int> publisherIds)
            : base($"Publishers with ids: {string.Join(", ", publisherIds)} don't exist in the database.") { }
    }
}