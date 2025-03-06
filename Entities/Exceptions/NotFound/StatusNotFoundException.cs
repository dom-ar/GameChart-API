namespace Entities.Exceptions.NotFound
{
    public sealed class StatusNotFoundException : NotFoundException
    {
        public StatusNotFoundException(int statusId)
            : base($"Status with id: {statusId} doesn't exist in the database.") { }
    }
}