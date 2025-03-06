namespace Entities.Exceptions.BadRequest
{
    public sealed class GamesCollectionBadRequest : BadRequestException
    {
        public GamesCollectionBadRequest()
            : base("Games collection sent from a client is null.") { }
    }
}
