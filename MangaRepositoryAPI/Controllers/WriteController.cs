using MangaRepositoryAPI.Data;
using MangaRepositoryAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangaRepositoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriteController(MangaRepoDbContext ctx, ILogger<WriteController> logger, Mapper mapper) : ControllerBase 
    {
        private readonly MangaRepoDbContext _ctx = ctx;
        private readonly ILogger<WriteController> _logger = logger;
        private readonly Mapper _mapper = mapper;

        #region Get

        [HttpGet]
        public IActionResult GetAll() 
        {
            try
            {

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
        public IActionResult Create([FromBody] WriteDTO dto) 
        {
            try
            {

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
        public IActionResult Update([FromBody] WriteDTO dto, [FromRoute] Guid id) 
        {
            try
            {

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
