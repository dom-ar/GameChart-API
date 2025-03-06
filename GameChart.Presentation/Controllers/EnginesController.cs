using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Entities.Exceptions.BadRequest;
using Shared.DataTransferObjects.Engine;
using Filters.ActionFilters;
using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures.EntityParameters;

namespace Presentation.Controllers
{
    [Route("api/engines")]
    [ApiController]
    public class EnginesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EnginesController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all engines
        public async Task<IActionResult> GetEngines([FromQuery] EngineParameters engineParameters)
        {
            var pagedResult = await _service.EngineService.GetAllEnginesAsync(engineParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.engines);
        }

        [HttpGet("{id:int}", Name = "EngineById")] // Get engine by id
        public async Task<IActionResult> GetEngine(int id)
        {
            var engine = await _service.EngineService.GetEngineAsync(id, trackChanges: false);
            return Ok(engine);
        }

        [HttpGet("collection/({ids})", Name = "EnginesCollection")] // Get a collection of engines
        public async Task<IActionResult> GetEnginesCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            var engines = await _service.EngineService.GetEnginesByIdsAsync(ids, trackChanges: false);
            return Ok(engines);
        }

        // POST

        [HttpPost] // Create an engine
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEngine([FromBody] EngineForCreationDto engine)
        {
            var createdEngine = await _service.EngineService.CreateEngineAsync(engine);
            
            return CreatedAtRoute("EngineById", new { id = createdEngine.Id }, createdEngine);
        }

        [HttpPost("collection")] // Create a collection of engines
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEnginesCollection([FromBody] IEnumerable<EngineForCreationDto>engines)
        {
            var result = await _service.EngineService.CreateEnginesCollectionAsync(engines);

            return CreatedAtRoute("EnginesCollection", new { result.ids }, result.engines);
        }

        // DELETE

        [HttpDelete("{engineId:int}")] // Delete an engine
        public async Task<IActionResult> DeleteEngine(int engineId)
        {
            await _service.EngineService.DeleteEngineAsync(engineId, trackChanges: false);

            return NoContent();
        }
    }
}