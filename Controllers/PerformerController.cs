using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.performer;
using liriksi.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace liriksi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformerController : ControllerBase
    {
        private readonly IPerformerService _service;

        public PerformerController(IPerformerService service)
        {
            _service = service;
        }
        [HttpGet("GetPerformers")]
        public ActionResult<List<Performer>> Get([FromQuery]PerformerSearchRequest obj)
        {
            return _service.Get(obj);
        }

        [HttpGet("GetPerformerById/{id}")]
        public Performer GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost("AddPerformer")]
        public ActionResult<Performer>Insert(PerformerInsertRequest obj)
        {
            return _service.Insert(obj);
        }
    
        [HttpPut]
        public Performer Update(int id, PerformerInsertRequest obj)
        {
            return _service.Update(id, obj);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}