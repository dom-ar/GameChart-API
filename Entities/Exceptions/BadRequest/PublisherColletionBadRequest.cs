namespace Entities.Exceptions.BadRequest
{
    public sealed class PublisherCollectionBadRequest : BadRequestException
    {
        public PublisherCollectionBadRequest()
            : base("Publisher collection sent from a client is null.") { }
    }
}