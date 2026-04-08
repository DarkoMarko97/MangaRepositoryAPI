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
        public IActionResult GetAll() { }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById() { }

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
