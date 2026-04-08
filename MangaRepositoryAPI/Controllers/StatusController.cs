using MangaRepositoryAPI.Data;
using MangaRepositoryAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MangaRepositoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController(MangaRepoDbContext ctx, ILogger<StatusController> logger, Mapper mapper) : ControllerBase
    {
        private readonly MangaRepoDbContext _ctx = ctx;
        private readonly ILogger<StatusController> _logger = logger;
        private readonly Mapper _mapper = mapper;

        #region Get

        /// <summary>
        /// Retrieves all status records as data transfer objects (DTOs).
        /// </summary>
        /// <remarks>If an internal server error occurs, the response will have HTTP status code 500 and
        /// include error details. The returned collection will be empty if no status records are found.</remarks>
        /// <returns>An <see cref="IActionResult"/> containing a collection of status DTOs with HTTP 200 (OK) if successful;
        /// otherwise, an error response with the appropriate HTTP status code.</returns>
        [HttpGet]
        public IActionResult GetAll() 
        {
            try
            {
                var result = _ctx.Statuses.ToList().ConvertAll(_mapper.MapEntityToDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Retrieves the status entity with the specified unique identifier.
        /// </summary>
        /// <remarks>The returned status entity includes related manga information. This endpoint is
        /// typically used to fetch a single status by its identifier for display or further processing.</remarks>
        /// <param name="id">The unique identifier of the status to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the status entity if found; otherwise, a 404 Not Found result.
        /// Returns a 500 Internal Server Error result if an unexpected error occurs.</returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            try
            {
                var result = _ctx.Statuses
                                 .Include(s => s.Manga)
                                 .SingleOrDefault(s => s.StatusId == id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
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
        /// Creates a new status entry using the provided data and returns the created status.
        /// </summary>
        /// <remarks>Returns a 201 response with the created status if the operation succeeds. Returns a
        /// 422 response if the status could not be created, or a 500 response if an unexpected error occurs.</remarks>
        /// <param name="dto">The data transfer object containing the information required to create a new status. Cannot be null.</param>
        /// <returns>An IActionResult containing the created status if successful; otherwise, an error response indicating the
        /// reason for failure.</returns>
        [HttpPost]
        public IActionResult Create([FromBody] StatusDTO dto) 
        {
            try
            {
                var result = new Status()
                {
                    StatusId = Guid.NewGuid(),
                    Description = dto.Description,
                    MangaId = dto.MangaId,
                    Manga = _ctx.Mangas.SingleOrDefault(m => m.MangaId == dto.MangaId)
                };
                _ctx.Statuses.Add(result);
                if (_ctx.SaveChanges() > 0)
                {
                    return  Ok(_mapper.MapEntityToDTO(result));
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
        /// Updates the status entity with the specified identifier using the provided data transfer object.
        /// </summary>
        /// <remarks>This method requires a valid status identifier and a non-null data transfer object.
        /// The update is applied to the status entity matching the provided identifier.</remarks>
        /// <param name="dto">The data transfer object containing the updated status information. Cannot be null.</param>
        /// <param name="id">The unique identifier of the status entity to update.</param>
        /// <returns>An IActionResult indicating the result of the update operation. Returns 200 OK if the update is successful,
        /// 422 Unprocessable Entity if the update fails, or 500 Internal Server Error if an unexpected error occurs.</returns>
        [HttpPut]
        public IActionResult Update([FromBody] StatusDTO dto,[FromRoute] Guid id) 
        {
            try
            {
                var result = _ctx.Statuses
                                 .Include(s => s.Manga)
                                 .SingleOrDefault(s => s.StatusId == id);
                result.Description = dto.Description;
                result.MangaId = dto.MangaId;
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
        /// Deletes the status entity with the specified identifier.
        /// </summary>
        /// <remarks>This action requires a valid status identifier. If the specified status is not found,
        /// a 404 Not Found response is returned. If the deletion fails due to a processing error, a 422 Unprocessable
        /// Entity response is returned. Any unexpected errors result in a 500 Internal Server Error response.</remarks>
        /// <param name="id">The unique identifier of the status to delete.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the delete operation. Returns <see cref="OkResult"/>
        /// if the status was deleted successfully, <see cref="NotFoundResult"/> if the status does not exist, <see
        /// cref="UnprocessableEntityResult"/> if the deletion could not be completed, or a server error result if an
        /// exception occurs.</returns>
        [HttpDelete]
        public IActionResult Delete([FromRoute] Guid id) 
        {
            try
            {
                var result = _ctx.Statuses
                                 .Include(s => s.Manga)
                                 .SingleOrDefault(s => s.StatusId == id);
                if (result == null) 
                {
                    return NotFound();
                }
                _ctx.Statuses.Remove(result);
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
