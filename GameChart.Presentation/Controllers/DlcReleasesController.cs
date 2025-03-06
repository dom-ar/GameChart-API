using Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects.DlcRelease;

namespace Presentation.Controllers
{
    [Route("api/games/{gameId}/dlc/{dlcId}/releases")]
    [ApiController]
    public class DlcReleasesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public DlcReleasesController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all releases for a dlc
        public async Task<IActionResult> GetDlcReleases(int gameId, int dlcId)
        {
            var dr = await _service.DlcReleaseService.GetDlcReleasesAsync(gameId, dlcId, trackChanges: false);

            return Ok(dr);
        }

        [HttpGet("{id:int}", Name = "ReleaseForDlc")] // Get a specific release for a dlc
        public async Task<IActionResult> GetDlcRelease(int gameId, int dlcId, int id)
        {
            var dr = await _service.DlcReleaseService.GetDlcReleaseAsync(gameId, dlcId, id, trackChanges: false);

            return Ok(dr);
        }

        [HttpGet("collection/({ids})", Name = "DlcReleasesCollection")] // Get a collection of releases for a dlc
        public async Task<IActionResult> GetDlcReleasesCollection(int gameId, int dlcId, 
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            var dr = await _service.DlcReleaseService.GetDlcReleasesByIdsAsync(gameId, dlcId, ids, trackChanges: false);

            return Ok(dr);
        }

        // POST

        [HttpPost] // Create a release for a dlc
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateReleaseForDlc(int gameId, int dlcId, [FromBody] DlcReleaseCreationDto dr)
        {
            var drToReturn = await _service.DlcReleaseService.CreateReleaseForDlcAsync(gameId, dlcId, dr, trackChanges: false);

            return CreatedAtRoute("ReleaseForDlc", new { gameId, dlcId, id = drToReturn.Id }, drToReturn);
        }

        [HttpPost("collection")] // Create a collection of releases for a dlc
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateReleaseCollectionForDlc(int gameId, int dlcId,
            [FromBody] IEnumerable<DlcReleaseCreationDto> drCollection)
        {
            var result = await _service.DlcReleaseService.CreateDlcReleasesCollectionAsync(gameId, dlcId, drCollection, trackChanges: false);

            return CreatedAtRoute("DlcReleasesCollection", new { gameId, dlcId, ids = result.ids }, result.dlcReleases);
        }

        // DELETE

        [HttpDelete("{drId:int}")] // Delete a release from dlc
        public async Task<IActionResult> DeleteReleaseFromDlc(int gameId, int dlcId, int drId)
        {
            await _service.DlcReleaseService.DeleteReleaseFromDlcAsync(gameId, dlcId, drId, trackChanges: false);
            return NoContent();
        }
    }
}