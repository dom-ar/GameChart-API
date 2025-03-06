using System.Text.Json;
using Entities.Exceptions.BadRequest;
using Filters.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects.Franchise;
using Shared.RequestFeatures.EntityParameters;

namespace Presentation.Controllers
{
    [Route("api/franchises")]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public FranchisesController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all franchises
        public async Task<IActionResult> GetFranchises([FromQuery] FranchiseParameters franchiseParameters)
        {
            var pagedResult = await _service.FranchiseService.GetAllFranchisesAsync(franchiseParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.franchises);
        }

        [HttpGet("{id:int}", Name = "FranchiseById")] // Get a single franchise by id 
        public async Task<IActionResult> GetFranchise(int id)
        {
            var franchise = await _service.FranchiseService.GetFranchiseAsync(id, trackChanges: false);

            return Ok(franchise);
        }

        [HttpGet("collection/({ids})", Name = "FranchiseCollection")]  // Get a collection of franchises by ids
        public async Task<IActionResult> GetFranchiseCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            var franchises = await _service.FranchiseService.GetFranchisesByIdsAsync(ids, trackChanges: false);

            return Ok(franchises);
        }

        // POST

        [HttpPost] // Create a franchise
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFranchise([FromBody] FranchiseForCreationDto franchise)
        {
            var createdFranchise = await _service.FranchiseService.CreateFranchiseAsync(franchise);

            return CreatedAtRoute("FranchiseById", new { id = createdFranchise.Id }, createdFranchise);
        }

        [HttpPost("collection")] // Create a collection of franchises
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFranchisesCollection([FromBody] IEnumerable<FranchiseForCreationDto> franchises)
        {
            var result = await _service.FranchiseService.CreateFranchisesCollectionAsync(franchises);

            return CreatedAtRoute("FranchiseCollection", new { result.ids }, result.franchises);
        }

        // DELETE

        [HttpDelete("{franchiseId:int}")] // Delete a franchise
        public async Task<IActionResult> DeleteFranchise(int franchiseId)
        {
            await _service.FranchiseService.DeleteFranchiseAsync(franchiseId, trackChanges: false);
            return NoContent();
        }
    }
}