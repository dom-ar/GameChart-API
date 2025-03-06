namespace Entities.Exceptions.BadRequest
{
    public sealed class PlatformCollectionBadRequest : BadRequestException
    {
        public PlatformCollectionBadRequest()
            : base("Platform collection sent from a client is null.") { }
    }
}