using System.Text.Json;
using Entities.Exceptions.BadRequest;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Presentation.ModelBinders;
using Shared.DataTransferObjects.Game;
using Filters.ActionFilters;
using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures.EntityParameters;

namespace Presentation.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public GamesController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all games with paging 
        public async Task<IActionResult> GetGames([FromQuery] GameParameters gameParameters)
        {
            var pagedResult = await _service.GameService.GetAllGamesAsync(gameParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.games);
        }

        [HttpGet("{id:int}", Name = "GameById")] // Get game by id
        public async Task<IActionResult> GetGame(int id)
        {
            var game = await _service.GameService.GetGameAsync(id, trackChanges: false);

            return Ok(game);
        }

        [HttpGet("collection/({ids})", Name = "GamesCollection")] // Get a collection of games
        public async Task<IActionResult> GetGamesCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<int> ids)
        {
            var games = await _service.GameService.GetGamesByIdsAsync(ids, trackChanges: false);

            return Ok(games);
        }

        // POST

        [HttpPost] // Create a game (can also add children: developers, genres)
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateGame([FromBody] GameForCreationDto game)
        {
            var createdGame = await _service.GameService.CreateGameAsync(game);

            return CreatedAtRoute("GameById", new { id = createdGame.Id }, createdGame);
        }

        [HttpPost("collection")] // Create a collection of games (can also add children to game: developers, genres)
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateGamesCollection([FromBody] IEnumerable<GameForCreationDto> games)
        {
            var result = await _service.GameService.CreateGamesCollectionAsync(games);

            return CreatedAtRoute("GamesCollection", new { result.ids }, result.games);
        }

        // PUT

        [HttpPut("{gameId:int}")] // Update a game, providing children will replace them with the provided ones, if not provided, they will be unchanged.
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateGame(int gameId, [FromBody] GameForUpdateDto game)
        {
            await _service.GameService.UpdateGameAsync(gameId, game, trackChanges: true);

            return NoContent();
        }

        // DELETE

        [HttpDelete("{gameId:int}")] // Delete a game
        public async Task<IActionResult> DeleteGame(int gameId)
        {
            await _service.GameService.DeleteGameAsync(gameId, trackChanges: false);

            return NoContent();
        }
    }
}