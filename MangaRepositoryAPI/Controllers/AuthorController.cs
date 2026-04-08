using MangaRepositoryAPI.Data;
using MangaRepositoryAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MangaRepositoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController(MangaRepoDbContext ctx, ILogger<AuthorController> logger, Mapper mapper) : ControllerBase
    {
        private readonly MangaRepoDbContext _ctx = ctx;
        private readonly ILogger<AuthorController> _logger = logger;
        private readonly Mapper _mapper = mapper;

        #region Get

        /// <summary>
        /// Retrieves all authors from the data store and returns them as a collection of DTOs.
        /// </summary>
        /// <remarks>If an internal server error occurs during retrieval, the method returns a response
        /// with status code 500.</remarks>
        /// <returns>An <see cref="IActionResult"/> containing a collection of author DTOs with HTTP status code 200 (OK) if
        /// successful; otherwise, an error response with the appropriate status code.</returns>
        [HttpGet]
        public IActionResult GetAll() 
        {
            try 
            {
                var result = _ctx.Authors.ToList().ConvertAll(_mapper.MapEntityToDTO) ;
                return Ok(result);
            } 
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex);
            }
        }


        /// <summary>
        /// Retrieves the details of an author by the specified unique identifier.
        /// </summary>
        /// <remarks>The response includes detailed information about the author and their associated
        /// mangas. This endpoint returns a 404 status code if the author does not exist.</remarks>
        /// <param name="id">The unique identifier of the author to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the detailed author data if found; otherwise, a 404 Not Found
        /// response. Returns a 500 Internal Server Error response if an unexpected error occurs.</returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            try
            {
                var result = _ctx.Authors
                                 .Include(a => a.Mangas)
                                 .SingleOrDefault(a => a.AuthorId == id);
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
        /// Creates a new author based on the provided data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object containing the information required to create a new author. Must not be null.</param>
        /// <returns>An IActionResult indicating the result of the operation. Returns 200 OK if the author is created
        /// successfully; 422 Unprocessable Entity if the creation fails; or 500 Internal Server Error if an unexpected
        /// error occurs.</returns>
        [HttpPost]
        public IActionResult Create([FromBody] AuthorDTO dto) 
        {
            try
            {
                var result = new Author()
                {
                    AuthorId = Guid.NewGuid(),
                    Name = dto.Name
                };
                _ctx.Authors.Add(result);
                if (_ctx.SaveChanges () > 0) 
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
        /// Updates the details of an existing author with the specified identifier using the provided data.
        /// </summary>
        /// <param name="dto">An object containing the updated author information. The object's properties are used to modify the existing
        /// author's details.</param>
        /// <param name="id">The unique identifier of the author to update.</param>
        /// <returns>An IActionResult indicating the result of the update operation. Returns 200 OK if the update is successful,
        /// 422 Unprocessable Entity if the update could not be performed, or 500 Internal Server Error if an unexpected
        /// error occurs.</returns>
        [HttpPut]
        public IActionResult Update([FromBody] AuthorDTO dto, [FromRoute] Guid id) 
        {
            try
            {
                var result = _ctx.Authors
                                 .Include(a => a.Mangas)
                                 .SingleOrDefault(a => a.AuthorId == id);
                result.Name = dto.Name;

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
        /// Deletes the author with the specified unique identifier.
        /// </summary>
        /// <remarks>This action also removes all related manga entities associated with the author. Use
        /// this endpoint to permanently delete an author and their related data.</remarks>
        /// <param name="id">The unique identifier of the author to delete.</param>
        /// <returns>An IActionResult indicating the result of the delete operation. Returns Ok if the author was deleted
        /// successfully, NotFound if the author does not exist, UnprocessableEntity if the deletion could not be
        /// completed, or StatusCode 500 if an internal error occurs.</returns>
        [HttpDelete]
        public IActionResult Delete([FromRoute] Guid id) 
        {
            try
            {
                var result = _ctx.Authors
                                 .Include(a => a.Mangas)
                                 .SingleOrDefault(a => a.AuthorId == id);
                if (result == null) 
                {
                    return NotFound();
                }
                _ctx.Authors.Remove(result);
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
