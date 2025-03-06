namespace Entities.Exceptions.NotFound
{
    public sealed class DlcReleaseNotFoundException : NotFoundException
    {
        public DlcReleaseNotFoundException(int dlcReleaseId)
            : base($"DlcRelease with id: {dlcReleaseId} doesn't exist in the database.") { }
    }
}