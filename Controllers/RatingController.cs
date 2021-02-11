using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.rates;
using liriksi.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace liriksi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _service;
        public RatingController(IRatingService service)
        {
            _service = service;
        }
        //todo GET top 10 rejtinga

        //get rates grouped by song and their average rates
        [HttpGet("GetSongRates")]
        public List<RateDetails> GetSongRates()
        {
            return _service.GetSongRates();
        }

        //get rates grouped by album and their average rates
        [HttpGet("GetAlbumRates")]
        public List<RateDetails> GetAlbumRates()
        {
            return _service.GetAlbumRates();
        }


        //get list of all users rate by song
        [HttpGet("GetRatesBySong/{songId}")]
        public List<UserSongRateGetRequest> GetRatesBySong(int songId)
        {
            return _service.GetRatesBySong(songId);
        }

        //get list of all users rate by album
        [HttpGet("GetRatesByAlbum/{albumId}")]
        public List<UserAlbumRateGetRequest> GetRatesByAlbum(int albumId)
        {
            return _service.GetRatesByAlbum(albumId);
        }
      

        [HttpPost("RateSong")]
        public bool RateSong(UsersSongRate obj)
        {
            return _service.RateSong(obj);
        }
       
        [HttpPost("RateAlbum")]
        public bool RateAlbum(UsersAlbumRate obj)
        {
            return _service.RateAlbum(obj);
        }


        [HttpGet("GetSongRatesByUser/{userId}")]
        public List<UserSongRateGetRequest> GetSongRatesByUser(int userId)
        {
           return _service.GetSongRatesByUser(userId);
        }

        [HttpGet("GetAlbumRatesByUser/{userId}")]
        public List<UserAlbumRateGetRequest> GetAlbumRatesByUser(int userId)
        {
            return _service.GetAlbumRatesByUser(userId);
        }

        [HttpGet("GetRateBySongByUser")]
        public UsersSongRate GetRateBySongByUser([FromQuery]HasUserRatedRequest obj)
        {
            UsersSongRate entity = _service.GetRateBySongByUser(obj);
            return entity;
        }

        [HttpGet("GetRateByAlbumByUser")]
        public UsersAlbumRate GetRateByAlbumByUser([FromQuery]HasUserRatedRequest obj)
        {
            return _service.GetRateByAlbumByUser(obj);
        }

    }
}