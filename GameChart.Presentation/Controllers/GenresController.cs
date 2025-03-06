using System.Text.Json;
using Entities.Exceptions.BadRequest;
using Filters.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects.Genre;
using Shared.RequestFeatures.EntityParameters;

namespace Presentation.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IServiceManager _service;

        public GenresController(IServiceManager service) => _service = service;

        // GET

        [HttpGet] // Get all genres
        public async Task<IActionResult> GetGenres([FromQuery] GenreParameters genreParameters)
        {
            var pagedResults = await _service.GenreService.GetAllGenresAsync(genreParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResults.metaData));

            return Ok(pagedResults.genres);
        }

        [HttpGet("{id:int}", Name = "GenreById")] // Get genre by id
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = await _service.GenreService.GetGenreAsync(id, trackChanges: false);
            
            return Ok(genre);
        }

        [HttpGet("collection/({ids})", Name = "GenreCollection")] // Get genre collection
        public async Task<IActionResult> GetGenreCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            var genres = await _service.GenreService.GetGenresByIdsAsync(ids, trackChanges: false);
           
            return Ok(genres);
        }

        // POST

        [HttpPost] // Create genre
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateGenre([FromBody] GenreForCreationDto genre)
        {
            var createdGenre = await _service.GenreService.CreateGenreAsync(genre);

            return CreatedAtRoute("GenreById", new { id = createdGenre.Id }, createdGenre);
        }

        [HttpPost("collection")] // Create a collection of genres
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateGenreCollection([FromBody] IEnumerable<GenreForCreationDto> genres)
        {
            var result = await _service.GenreService.CreateGenresCollectionAsync(genres);

            return CreatedAtRoute("GenreCollection", new { result.ids }, result.genres);
        }

        // DELETE

        [HttpDelete("{genreId:int}")] // Delete genre
        public async Task<IActionResult> DeleteGenre(int genreId)
        {
            await _service.GenreService.DeleteGenreAsync(genreId, trackChanges: false);

            return NoContent();
        }

    }
}