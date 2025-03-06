using System.Text.Json;
using Entities.Exceptions.BadRequest;
using Filters.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects.Publisher;
using Shared.RequestFeatures.EntityParameters;

namespace Presentation.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public PublishersController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all publishers
        public async Task<IActionResult> GetPublishers([FromQuery] PublisherParameters publisherParameters)
        {
            var pagedResults = await _service.PublisherService.GetAllPublishersAsync(publisherParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResults.metaData));

            return Ok(pagedResults.publishers);
        }

        [HttpGet("{id:int}", Name = "PublisherById")] // Get publisher by id
        public async Task<IActionResult> GetPublisher(int id)
        {
            var publisher = await _service.PublisherService.GetPublisherAsync(id, trackChanges: false);

            return Ok(publisher);
        }

        [HttpGet("collection/({ids})", Name = "PublisherCollection")] // Get a collection of publishers
        public async Task<IActionResult> GetPublishersCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids) 
        {
            var publishers = await _service.PublisherService.GetPublishersByIdsAsync(ids, trackChanges: false);
            return Ok(publishers);
        }

        // POST

        [HttpPost] // Create a publisher
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePublisher([FromBody] PublisherForCreationDto publisher)
        {
            var createdPublisher = await _service.PublisherService.CreatePublisherAsync(publisher);

            return CreatedAtRoute("PublisherById", new { id = createdPublisher.Id }, createdPublisher);
        }

        [HttpPost("collection")] // Create a collection of publishers
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePublishersCollection([FromBody] IEnumerable<PublisherForCreationDto> publishers)

        {
            var result = await _service.PublisherService.CreatePublishersCollectionAsync(publishers);

            return CreatedAtRoute("PublisherCollection", new { result.ids }, result.publishers);
        }

        // DELETE

        [HttpDelete("{publisherId:int}")] // Delete a publisher
        public async Task<IActionResult> DeletePublisher(int publisherId)
        {
            await _service.PublisherService.DeletePublisherAsync(publisherId, trackChanges: false);

            return NoContent();
        }
    }
}