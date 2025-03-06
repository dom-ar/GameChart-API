namespace Entities.Exceptions.BadRequest
{
    public sealed class StatusCollectionBadRequest : BadRequestException
    {
        public StatusCollectionBadRequest()
            : base("Status collection sent from a client is null.") { }
    }
}