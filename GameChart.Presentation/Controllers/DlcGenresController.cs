using Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DlcGenres;

namespace Presentation.Controllers
{
    [Route("api/games/{gameId}/dlc/{dlcId}/genres")]
    [ApiController]
    public class DlcGenresController : ControllerBase
    {
        private readonly IServiceManager _service;

        public DlcGenresController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all genres for a dlc
        public async Task<IActionResult> GetDlcGenres(int gameId, int dlcId)
        {
            var dlcGenres = await _service.DlcGenresService.GetDlcGenresAsync(gameId, dlcId, trackChanges: false);
            return Ok(dlcGenres);
        }

        // POST

        [HttpPost] // Add a genre to a dlc
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddGenreToDlc(int gameId, int dlcId, [FromBody] DlcGenresForCreationDto dlcGenres)
        {
            var dlcGenresToReturn = await _service.DlcGenresService.AddGenreToDlcAsync(gameId, dlcId, dlcGenres, trackChanges: false);

            return Ok(dlcGenresToReturn);
        }

        [HttpPost("collection")] // Add a collection of genres to a dlc
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddDlcGenresCollection(int gameId, int dlcId, [FromBody] IEnumerable<DlcGenresForCreationDto> dlcGenresCollection)
        {
            var result = await _service.DlcGenresService.AddDlcGenresCollectionAsync(gameId, dlcId, dlcGenresCollection, trackChanges: false);
            
            return Ok(result);
        }

        // DELETE

        [HttpDelete("{genreId:int}")] // Remove a genre from a dlc
        public async Task<IActionResult> RemoveGenreFromDlc(int gameId, int dlcId, int genreId)
        {
            await _service.DlcGenresService.RemoveGenreFromDlcAsync(gameId, dlcId, genreId, trackChanges: false);
            return NoContent();
        }

    }
}