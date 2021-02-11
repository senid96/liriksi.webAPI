using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using liriksi.Model;
using liriksi.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace liriksi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _service;
        public LocationController(ILocationService service)
        {
            _service = service;
        }

        [HttpGet("GetCitiesByCountryId/{countryId}")]
        public ActionResult<List<City>> GetCitiesByCountryId(int countryId)
        {
            return _service.GetCitiesByCountryId(countryId);
        }

        [HttpGet("GetCountries")]
        public ActionResult<List<Country>> GetCountries()
        {
            return _service.GetCountries();
        }
    }
}