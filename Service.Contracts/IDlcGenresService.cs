using Shared.DataTransferObjects.DlcGenres;

namespace Service.Contracts
{
    public interface IDlcGenresService
    {
        // GET
        Task<IEnumerable<DlcGenresDto>> GetDlcGenresAsync(int gameId, int genreId, bool trackChanges);
        // POST
        Task<DlcGenresDto> AddGenreToDlcAsync(int gameId, int genreId, DlcGenresForCreationDto dlcGenres, bool trackChanges);

        Task<(IEnumerable<DlcGenresDto> dlcGenres, string ids)> AddDlcGenresCollectionAsync(int gameId, int genreId,
            IEnumerable<DlcGenresForCreationDto> dlcGenresCollection, bool trackChanges);
        // DELETE
        Task RemoveGenreFromDlcAsync(int gameId, int dlcId, int genreId, bool trackChanges);
    }
}