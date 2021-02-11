using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using liriksi.Model;
using liriksi.WebAPI.Services;
using liriksi.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace liriksi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService service)
        {
            _genreService = service;
        }

        [HttpGet("GetGenres")]
        public ActionResult<List<Genre>> Get([FromQuery]string genre)
        {
            return _genreService.Get(genre);
        }

        [HttpGet("{id}")]
        public ActionResult<Genre> Get(int id)
        {
            return _genreService.GetById(id);
        }

        [HttpPut]
        public Genre Update(int id, string name) 
        {
            return _genreService.Update(id, name);
        }

        [HttpPost("AddGenre")]
        public ActionResult<Genre> Insert(Genre genre)
        {          
            return _genreService.Insert(genre);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return _genreService.Delete(id);
        }
    }
}