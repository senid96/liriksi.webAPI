using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.album;
using liriksi.WebAPI.Services;
using liriksi.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace liriksi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _service;
        public AlbumController(IAlbumService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Album>> Get([FromQuery]AlbumSearchRequest searchReq)
        {
            return _service.Get(searchReq);
        }

        [HttpGet("GetAlbumById/{id}")]
        public ActionResult<Album>GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost("InsertAlbum")]
        public ActionResult<Album> Insert([FromBody]AlbumInsertRequest album)
        {
            return _service.Insert(album);
        }

        [HttpPut("{Id}")]
        public ActionResult<Album> Update(int id, AlbumInsertRequest album)
        {
            return _service.Update(id, album);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return _service.Delete(id);
        }

        [HttpGet("GetAlbumsByPerformerId")]
        public ActionResult<List<Album>> GetAlbumsByPerformerId(int id)
        {
            return _service.GetAlbumsByPerformerId(id);
        }
    }
}