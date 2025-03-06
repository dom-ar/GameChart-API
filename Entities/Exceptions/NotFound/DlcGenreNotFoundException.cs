namespace Entities.Exceptions.NotFound
{
    public sealed class DlcGenreNotFoundException : NotFoundException
    {
        public DlcGenreNotFoundException(int dlcId, int genreId)
            : base($"The genre with id: {genreId} is not part of the dlc with id: {dlcId}.")
        {
        }
    }
}