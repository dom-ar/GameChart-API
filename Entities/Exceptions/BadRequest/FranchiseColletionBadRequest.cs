namespace Entities.Exceptions.BadRequest
{
    public sealed class FranchiseCollectionBadRequest : BadRequestException
    {
        public FranchiseCollectionBadRequest()
            : base("Franchise collection sent from a client is null.") { }
    }
}