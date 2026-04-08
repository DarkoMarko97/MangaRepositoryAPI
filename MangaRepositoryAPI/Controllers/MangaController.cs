using MangaRepositoryAPI.Data;
using MangaRepositoryAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangaRepositoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaController(MangaRepoDbContext ctx ,ILogger<MangaController> logger, Mapper mapper) : ControllerBase
    {
        private readonly MangaRepoDbContext _ctx = ctx;
        private readonly ILogger<MangaController> _logger = logger;
        private readonly Mapper _mapper = mapper;

        #region Get

        [HttpGet]
        public IActionResult GetAll() { }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById() { }

        #endregion

        #region GetSpecific

        [HttpGet]
        [Route("GetByAuthor/{Authors}")]
        public IActionResult GetByAuthor() { }

        [HttpGet]
        [Route("GetByGenres/{Genres}")]
        public IActionResult GetByGenres() { }

        [HttpGet]
        [Route("GetByStatus/{Status}")]
        public IActionResult GetByStatus() { }

        #endregion

        #region Post

        [HttpPost]
        public IActionResult Create() { }

        #endregion

        #region Put

        [HttpPut]
        public IActionResult Update() { }

        #endregion

        #region Delete

        [HttpDelete]
        public IActionResult Delete() { }

        #endregion
    }
}
