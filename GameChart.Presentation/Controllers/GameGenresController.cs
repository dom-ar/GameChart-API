using Entities.Exceptions.BadRequest;
using Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects.GameGenres;

namespace Presentation.Controllers
{
    [Route("api/games/{gameId}/genres")]
    [ApiController]
    public class GameGenresController : ControllerBase
    {
        private readonly IServiceManager _service;

        public GameGenresController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all genres for a game
        public async Task<IActionResult> GetGameGenres(int gameId)
        {
            var genres = await _service.GameGenresService.GetGameGenresAsync(gameId, trackChanges: false);
            
            return Ok(genres);
        }

        [HttpGet("collection/({ids})", Name = "GameGenresCollection")] // Get a collection of game genres for a game
        public async Task<IActionResult> GetGameGenresCollection(int gameId, [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            var gameGenres = await _service.GameGenresService.GetGameGenresByIdsAsync(gameId, ids, trackChanges: false);
            
            return Ok(gameGenres);
        }

        // POST

        [HttpPost] // Add a genre to a game
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddGenreToGame(int gameId, [FromBody] GameGenresForCreationDto gameGenres)
        {
            var gameGenresToReturn = await _service.GameGenresService.AddGenreToGameAsync(gameId, gameGenres, trackChanges: false);

            return Ok(gameGenresToReturn);
        }

        [HttpPost("collection")] // Add a collection of genres to a game
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddGamesGenresCollection(int gameId, [FromBody] IEnumerable<GameGenresForCreationDto> gameGenresCollection)
        {
            var result = await _service.GameGenresService.AddGamesGenresCollectionAsync(gameId, gameGenresCollection, trackChanges: false);
            
            return CreatedAtRoute("GameGenresCollection", new { gameId, ids = result.ids }, result.gameGenres);
        }

        // DELETE

        [HttpDelete("{genreId:int}")] // Remove a genre from a game
        public async Task<IActionResult> GetGenreGames(int gameId, int genreId)
        {
            await _service.GameGenresService.RemoveGenreFromGameAsync(gameId, genreId, trackChanges: false);

            return NoContent();
        }
    }
}