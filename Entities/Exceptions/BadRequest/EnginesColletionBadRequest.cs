namespace Entities.Exceptions.BadRequest
{
    public sealed class EngineCollectionBadRequest : BadRequestException
    {
        public EngineCollectionBadRequest()
            : base("Engine collection sent from a client is null.") { }
    }
}