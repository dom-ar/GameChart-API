namespace Entities.Exceptions.NotFound
{
    public class DlcNotFoundException : NotFoundException
    {
        public DlcNotFoundException(int gameId, int dlcId)
            : base($"Dlc with id: {dlcId} doesn't belong to  Game with id: {gameId} or doesn't exist in the database")
        {
        }
    }
}
