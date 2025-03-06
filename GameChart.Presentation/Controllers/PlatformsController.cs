using System.Text.Json;
using Entities.Exceptions.BadRequest;
using Filters.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Platform;
using Shared.RequestFeatures.EntityParameters;

namespace Presentation.Controllers
{
    [Route("api/platforms")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public PlatformsController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all platforms
        public async Task<IActionResult> GetPlatforms([FromQuery] PlatformParameters platformParameters)
        {
            var pagedResult = await _service.PlatformService.GetAllPlatformsAsync(platformParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.platforms);
        }

        [HttpGet("{id:int}", Name = "PlatformById")] // Get platform by id
        public async Task<IActionResult> GetPlatform(int id)
        {
            var platform = await _service.PlatformService.GetPlatformAsync(id, trackChanges: false);
            return Ok(platform);
        }

        [HttpGet("collection/({ids})", Name = "PlatformCollection")] // Get platform collection
        public async Task<IActionResult> GetPlatformCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            var platforms = await _service.PlatformService.GetPlatformsByIdsAsync(ids, trackChanges: false);
            return Ok(platforms);
        }

        // POST

        [HttpPost] // Create platform
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePlatform([FromBody] PlatformForCreationDto platform)
        {
            var createdPlatform = await _service.PlatformService.CreatePlatformAsync(platform);
            
            return CreatedAtRoute("PlatformById", new { id = createdPlatform.Id }, createdPlatform);
        }

        [HttpPost("collection")] // Create a collection of platforms
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePlatformCollection([FromBody] IEnumerable<PlatformForCreationDto> platforms)
        {
            var result = await _service.PlatformService.CreatePlatformsCollectionAsync(platforms);

            return CreatedAtRoute("PlatformCollection", new { result.ids }, result.platforms);
        }

        // DELETE 

        [HttpDelete("{platformId:int}")] // Delete platform
        public async Task<IActionResult> DeletePlatform(int platformId)
        {
            await _service.PlatformService.DeletePlatformAsync(platformId, trackChanges: false);

            return NoContent();
        }
    }
}