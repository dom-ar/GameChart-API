using System.Text.Json;
using Filters.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects.Developer;
using Shared.RequestFeatures.EntityParameters;

namespace Presentation.Controllers
{
    [Route("api/developers")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public DevelopersController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all developers with paging 
        public async Task<IActionResult> GetDevelopers([FromQuery] DeveloperParameters developerParameters)
        {
            var pagedResult = await _service.DeveloperService.GetAllDevelopersAsync(developerParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.developers);
        }

        [HttpGet("{id:int}", Name = "DeveloperById")] // Get a single developer by id
        public async Task<IActionResult> GetDeveloper(int id)
        {
            var developer = await _service.DeveloperService.GetDeveloperAsync(id, trackChanges: false);
            return Ok(developer);
        }

        [HttpGet("collection/({ids})", Name = "DevelopersCollection")] // Get a collection of developers by ids
        public async Task<IActionResult> GetDeveloper([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            var developers = await _service.DeveloperService.GetDevelopersByIdsAsync(ids, trackChanges: false);
            return Ok(developers);
        }

        // POST

        [HttpPost] // Create a developer
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDeveloper([FromBody] DeveloperForCreationDto developer)
        {
            var createdDeveloper = await _service.DeveloperService.CreateDeveloperAsync(developer);
            
            return CreatedAtRoute("DeveloperById", new { id = createdDeveloper.Id }, createdDeveloper);
        }

        [HttpPost("collection")] // Create a collection of developers
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDevelopersCollection([FromBody] IEnumerable<DeveloperForCreationDto> developers)
        {
            var result = await _service.DeveloperService.CreateDevelopersCollectionAsync(developers);
            return CreatedAtRoute("DevelopersCollection", new { result.ids }, result.developers);
        }

        // DELETE

        [HttpDelete("{developerId:int}")] // Delete a developer
        public async Task<IActionResult> DeleteDeveloper(int developerId)
        {
            await _service.DeveloperService.DeleteDeveloperAsync(developerId, trackChanges: false);
            return NoContent();
        }
    }
}