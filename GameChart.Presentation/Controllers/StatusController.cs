using Entities.Exceptions.BadRequest;
using Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects.Status;

namespace Presentation.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IServiceManager _service;

        public StatusController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all statuses
        public async Task<IActionResult> GetStatus()
        {
            var status = await _service.StatusService.GetAllStatusAsync(trackChanges: false);
            return Ok(status);
        }

        [HttpGet("{id:int}", Name = "StatusById")] // Get status by id
        public async Task<IActionResult> GetStatus(int id)
        {
            var status = await _service.StatusService.GetStatusAsync(id, trackChanges: false);
            return Ok(status);
        }

        [HttpGet("collection/({ids})", Name = "StatusCollection")] // Get status collection
        public async Task<IActionResult> GetStatusCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            var statuses = await _service.StatusService.GetStatusesByIdsAsync(ids, trackChanges: false);
            return Ok(statuses);
        }

        // POST

        [HttpPost] // Create a status
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateStatus([FromBody] StatusForCreationDto status)
        {
            var createdStatus = await _service.StatusService.CreateStatusAsync(status);

            return CreatedAtRoute("StatusById", new { id = createdStatus.Id }, createdStatus);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateStatusCollection([FromBody] IEnumerable<StatusForCreationDto> statuses)
        {
            var result = await _service.StatusService.CreateStatusesCollectionAsync(statuses);

            return CreatedAtRoute("StatusCollection", new { result.ids }, result.statuses);
        }

        // DELETE

        [HttpDelete("{statusId:int}")] // Delete a status
        public async Task<IActionResult> DeleteStatus(int statusId)
        {
            await _service.StatusService.DeleteStatusAsync(statusId, trackChanges: false);

            return NoContent();
        }

    }
}