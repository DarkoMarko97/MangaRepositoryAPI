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
