using Shared.DataTransferObjects.DlcDevelopers;

namespace Service.Contracts
{
    public interface IDlcDevelopersService
    {
        // GET
        Task<IEnumerable<DlcDevelopersDto>> GetDlcDevelopersAsync(int gameId, int dlcId, bool trackChanges);
        // POST
        Task<DlcDevelopersDto> AddDeveloperToDlcAsync(int gameId, int dlcId, DlcDevelopersForCreationDto dlcDevelopers, bool trackChanges);

        Task<(IEnumerable<DlcDevelopersDto> dlcDevelopers, string ids)> AddDlcDevelopersCollectionAsync(int gameId, int dlcId,
            IEnumerable<DlcDevelopersForCreationDto> dlcDevelopersCollection, bool trackChanges);
        // DELETE
        Task RemoveDeveloperFromDlcAsync(int gameId, int dlcId, int developerId, bool trackChanges);
    }
}