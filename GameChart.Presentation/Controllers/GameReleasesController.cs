using Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects.GameRelease;

namespace Presentation.Controllers
{
    [Route("api/games/{gameId}/releases")]
    [ApiController]
    public class GameReleasesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public GameReleasesController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all releases for a game
        public async Task<IActionResult> GetGameReleases(int gameId)
        {
            var gr = await _service.GameReleaseService.GetGameReleasesAsync(gameId, trackChanges: false);
            
            return Ok(gr);
        }

        [HttpGet("{id:int}", Name = "ReleaseForGame")] // Get a single release for a game
        public async Task<IActionResult> GetGameRelease(int gameId, int id)
        {
            var gr = await _service.GameReleaseService.GetGameReleaseAsync(gameId, id, trackChanges: false);
            
            return Ok(gr);
        }

        [HttpGet("collection/({ids})", Name = "GameReleasesCollection")] // Get a collection of releases for a game
        public async Task<IActionResult> GetGameReleasesCollection(int gameId,
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            var gr = await _service.GameReleaseService.GetGameReleasesByIdsAsync(gameId, ids, trackChanges: false);
            
            return Ok(gr);
        }

        // POST

        [HttpPost] // Create a release for a game
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateReleaseForGame(int gameId, [FromBody] GameReleaseCreationDto gr)
        {
            var grToReturn = await _service.GameReleaseService.CreateReleaseForGameAsync(gameId, gr, trackChanges: false);

            return CreatedAtRoute("ReleaseForGame", new { gameId, id = grToReturn.Id }, grToReturn);
        }

        [HttpPost("collection")] // Create a collection of releases for a game
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateReleaseCollectionForGame(int gameId,
            [FromBody] IEnumerable<GameReleaseCreationDto> grCollection)
        {
            var result = await _service.GameReleaseService.CreateGameReleasesCollectionAsync(gameId, grCollection, trackChanges: false);
            
            return CreatedAtRoute("GameReleasesCollection", new { gameId, ids = result.ids }, result.gameRelease);
        }

        // DELETE

        [HttpDelete("{grId:int}")] // Delete a release from a game
        public async Task<IActionResult> DeleteReleaseFromGame(int gameId, int grId)
        {
            await _service.GameReleaseService.DeleteReleaseFromGameAsync(gameId, grId, trackChanges: false);

            return NoContent();
        }
    }
}