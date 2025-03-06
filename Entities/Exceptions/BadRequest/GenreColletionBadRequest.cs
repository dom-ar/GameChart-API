namespace Entities.Exceptions.BadRequest
{
    public sealed class GenreCollectionBadRequest : BadRequestException
    {
        public GenreCollectionBadRequest()
            : base("Genre collection sent from a client is null.") { }
    }
}