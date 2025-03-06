using Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects.GameDevelopers;

namespace Presentation.Controllers
{
    [Route("api/games/{gameId}/developers")]
    [ApiController]
    public class GameDevelopersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public GameDevelopersController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all developers for a game
        public async Task<IActionResult> GetGameDevelopers(int gameId)
        {
            var dev = await _service.GameDevelopersService.GetGameDevelopersAsync(gameId, trackChanges: false);

            return Ok(dev);
        }

        [HttpGet("collection/({ids})", Name = "GameDevelopersCollection")] // Get a collection of developers for a game
        public async Task<IActionResult> GetGameDevelopersCollection(int gameId,
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            var gameDevs = await _service.GameDevelopersService.GetGameDevelopersByIdsAsync(gameId, ids, trackChanges: false);

            return Ok(gameDevs);
        }

        // POST

        [HttpPost] // add a developer to a game
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddDeveloperToGame(int gameId, [FromBody] GameDevelopersForCreationDto gameDevelopers)
        {
            var gameDevsToReturn = await _service.GameDevelopersService.AddDeveloperToGameAsync(gameId, gameDevelopers, trackChanges: false);

            return Ok(gameDevsToReturn);
        }

        [HttpPost("collection")] // add a collection of developers to a game
        public async Task<IActionResult> AddGameDevelopersCollection(int gameId, [FromBody] IEnumerable<GameDevelopersForCreationDto> gameDevelopersCollection)
        {
            var result = await _service.GameDevelopersService.AddGameDevelopersCollectionAsync(gameId, gameDevelopersCollection, trackChanges: false);
            
            return CreatedAtRoute("GameDevelopersCollection", new { gameId, ids = result.ids }, result.gameDevelopers);
        }

        // DELETE

        [HttpDelete("{devId:int}")] // remove a developer from a game
        public async Task<IActionResult> RemoveDeveloperFromGame(int gameId, int devId)
        {
            await _service.GameDevelopersService.RemoveDeveloperFromGameAsync(gameId, devId, trackChanges: false);

            return NoContent();
        }
    }
}
