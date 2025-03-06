namespace Entities.Exceptions.NotFound
{
    public sealed class DlcDeveloperNotFoundException : NotFoundException
    {
        public DlcDeveloperNotFoundException(int dlcId, int developerId)
            : base($"The developer with id: {developerId} is not part of the dlc with id: {dlcId}.")
        {
        }
    }
}