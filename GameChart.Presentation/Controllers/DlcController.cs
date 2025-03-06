using System.Text.Json;
using Filters.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Dlc;
using Shared.RequestFeatures.EntityParameters;

namespace Presentation.Controllers
{
    [Route("api/games/{gameId}/dlc")]
    [ApiController]
    public class DlcsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public DlcsController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all dlc for a specific game
        public async Task<IActionResult> GetGameDlcs(int gameId, [FromQuery] DlcParameters dlcParameters)
        {
            var pagedResult = await _service.DlcService.GetDlcsAsync(gameId, dlcParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.dlcs);
        }

        [HttpGet("/api/dlc")] // Get all the dlcs across all games
        public async Task<IActionResult> GetAllDlcs([FromQuery] DlcParameters dlcParameters)
        {
            var pagedResult = await _service.DlcService.GetAllDlcsAsync(dlcParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.dlcs);
        }

        [HttpGet("{id:int}", Name = "GetDlcForGame")] // Get a specific dlc for a specific game
        public async Task<IActionResult> GetGameDlc(int gameId, int id)
        {
            var dlc = await _service.DlcService.GetDlcAsync(gameId, id, trackChanges: false);
            return Ok(dlc);
        }

        // POST

        [HttpPost] // Create a dlc for a specific game
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDlcForGame(int gameId, [FromBody] DlcForCreationDto dlc)
        {
            var dlcToReturn = await _service.DlcService.CreateDlcForGameAsync(gameId, dlc, trackChanges: false);
            
            return CreatedAtRoute("GetDlcForGame", new { gameId, id = dlcToReturn.Id }, dlcToReturn);
        }

        [HttpPost("collection")] // Create a collection of dlcs for a specific game
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddDlcToGameCollection(int gameId,
            [FromBody] IEnumerable<DlcForCreationDto> dlcCollection)
        {
            var result = await _service.DlcService.CreateDlcCollectionForGameAsync(gameId, dlcCollection, trackChanges: false);
            
            return Ok(result);
        }

        // PUT

        [HttpPut("{id:int}")] // Update a specific dlc for a specific game
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDlc(int gameId, int id, [FromBody] DlcForUpdateDto dlcForUpdate)
        {
            await _service.DlcService.UpdateDlcAsync(gameId, id, dlcForUpdate, gameTrackChanges: false, dlcTrackChanges: true);
            
            return NoContent();
        }

        // DELETE

        [HttpDelete("{dlcId:int}")] // Delete a specific dlc for a specific game
        public async Task<IActionResult> DeleteDlcFromGame(int gameId, int dlcId)
        {
            await _service.DlcService.DeleteDlcFromGameAsync(gameId, dlcId, trackChanges: false);

            return NoContent();
        }
    }
}