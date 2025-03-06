using Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DlcDevelopers;

namespace Presentation.Controllers
{
    [Route("api/games/{gameId}/dlc/{dlcId}/developers")]
    [ApiController]
    public class DlcDevelopersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public DlcDevelopersController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all developers for a dlc
        public async Task<IActionResult> GetDlcDevelopers(int gameId, int dlcId)
        {
            var dlcDevs = await _service.DlcDevelopersService.GetDlcDevelopersAsync(gameId, dlcId, trackChanges: false);

            return Ok(dlcDevs);
        }

        // POST

        [HttpPost] // Add a developer to a dlc
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddDeveloperToDlc(int gameId, int dlcId, [FromBody] DlcDevelopersForCreationDto dlcDevelopers)
        {
            var dlcDevsToReturn = await _service.DlcDevelopersService.AddDeveloperToDlcAsync(gameId, dlcId, dlcDevelopers, trackChanges: false);

            return Ok(dlcDevsToReturn);
        }

        [HttpPost("collection")] // Add a collection of developers to a dlc
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddDlcDevelopersCollection(int gameId, int dlcId, [FromBody] IEnumerable<DlcDevelopersForCreationDto> dlcDevelopersCollection)
        {
            var result = await _service.DlcDevelopersService.AddDlcDevelopersCollectionAsync(gameId, dlcId, dlcDevelopersCollection, trackChanges: false);
            
            return Ok(result);
        }

        // DELETE

        [HttpDelete("{devId:int}")] // Remove a developer from a dlc
        public async Task<IActionResult> RemoveGenreFromDlc(int gameId, int dlcId, int devId)
        {
            await _service.DlcDevelopersService.RemoveDeveloperFromDlcAsync(gameId, dlcId, devId, trackChanges: false);

            return NoContent();
        }
    }
}