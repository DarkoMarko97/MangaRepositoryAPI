using MangaRepositoryAPI.Data;
using MangaRepositoryAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MangaRepositoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController(MangaRepoDbContext ctx, ILogger<GenreController> logger, Mapper mapper) : ControllerBase
    {
        private readonly MangaRepoDbContext _ctx = ctx;
        private readonly ILogger<GenreController> _logger = logger;
        private readonly Mapper _mapper = mapper;

        #region Get

        /// <summary>
        /// Retrieves all genres as data transfer objects.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing a collection of genre DTOs with HTTP 200 (OK) if successful;
        /// otherwise, an error response with the appropriate HTTP status code.</returns>
        [HttpGet]
        public IActionResult GetAll() 
        {
            try
            {
                var result = _ctx.Genres.ToList().ConvertAll(_mapper.MapEntityToDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Retrieves the details of a genre by its unique identifier.
        /// </summary>
        /// <remarks>Returns a 500 Internal Server Error if an unexpected error occurs during
        /// processing.</remarks>
        /// <param name="id">The unique identifier of the genre to retrieve.</param>
        /// <returns>An IActionResult containing the detailed genre data if found; otherwise, a NotFound result if the genre does
        /// not exist.</returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            try
            {
                var result = _ctx.Genres
                                 .Include(g => g.Mangas)
                                 .SingleOrDefault(g => g.GenreId == id);
                if (result == null) 
                {
                    return NotFound();
                }
                return Ok(_mapper.MapEntityToDetailedDTO(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex);
            }
        }

        #endregion

        #region Post


        /// <summary>
        /// Creates a new genre using the provided data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object containing the information required to create a new genre. Must not be null.</param>
        /// <returns>An IActionResult indicating the result of the create operation. Returns 200 OK if the genre is created
        /// successfully; 422 Unprocessable Entity if the creation fails; or 500 Internal Server Error if an unexpected
        /// error occurs.</returns>
        [HttpPost]
        public IActionResult Create([FromBody] GenreDTO dto) 
        {
            try
            {
                var result = new Genre() 
                {
                    GenreId = Guid.NewGuid(),
                    GenreName = dto.GenreName
                };
                _ctx.Genres.Add(result);
                if (_ctx.SaveChanges() > 0) 
                {
                    return Ok();
                }
                else 
                {
                    return UnprocessableEntity();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex);
            }
        }

        #endregion

        #region Put

        /// <summary>
        /// Updates the details of an existing genre with the specified identifier.
        /// </summary>
        /// <remarks>The request must include a valid genre identifier and a non-null GenreDTO in the
        /// request body. The operation updates only the genre's name. If the genre does not exist, the method returns
        /// 422 Unprocessable Entity.</remarks>
        /// <param name="dto">The data transfer object containing the updated genre information. Cannot be null.</param>
        /// <param name="id">The unique identifier of the genre to update.</param>
        /// <returns>An IActionResult indicating the result of the update operation. Returns 200 OK if the update is successful,
        /// 422 Unprocessable Entity if the update fails, or 500 Internal Server Error if an unexpected error occurs.</returns>
        [HttpPut]
        public IActionResult Update([FromBody] GenreDTO dto, [FromRoute] Guid id) 
        {
            try
            {
                var result = _ctx.Genres
                                 .Include(g => g.Mangas)
                                 .SingleOrDefault(g => g.GenreId == id);
                result.GenreName = dto.GenreName;
                if (_ctx.SaveChanges() > 0) 
                {
                    return Ok();
                }
                else 
                {
                    return UnprocessableEntity();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the genre with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the genre to delete.</param>
        /// <returns>An <see cref="OkResult"/> if the genre was successfully deleted; a <see cref="NotFoundResult"/> if the genre
        /// does not exist; an <see cref="UnprocessableEntityResult"/> if the deletion could not be completed; or a <see
        /// cref="ObjectResult"/> with status code 500 if an unexpected error occurs.</returns>
        [HttpDelete]
        public IActionResult Delete([FromRoute] Guid id) 
        {
            try
            {
                var result = _ctx.Genres
                                 .Include(g => g.Mangas)
                                 .SingleOrDefault(g => g.GenreId == id);
                if (result == null) 
                {
                    return NotFound();
                }
                _ctx.Genres.Remove(result);
                if (_ctx.SaveChanges() > 0) 
                {
                    return Ok();
                }
                else 
                {
                    return UnprocessableEntity();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex);
            }
        }

        #endregion
    }
}
