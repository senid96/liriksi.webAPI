using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.user;
using liriksi.WebAPI.Services;
using liriksi.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace liriksi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        //search by name, or get all
        [HttpGet]
        public ActionResult<List<UserGetRequest>>Get([FromQuery]UserSearchRequest obj)
        {
            return _service.Get(obj);
        }

        [HttpGet("{id}")]
        public UserGetRequest Get(int id)
        {
            return _service.Get(id);
        }
        
        [Authorize(Roles ="Admin")]
        [HttpPost("InsertUser")]
        public ActionResult<UserGetRequest>Insert(UserInsertRequest obj)
        {
            return _service.Insert(obj);
        }

        [HttpPut("{id}")]
        public UserGetRequest Update(int id, UserUpdateRequest obj)
        {
           return _service.Update(id, obj);
        }

        //obratiti paznju sa api service kako ce ovo raditi
        /*[HttpPatch("ChangeUserStatus")]
        public bool ChangeUserStatus(int id, bool status)
        {
            return _service.ChangeUserStatus(id, status);
        }*/

        [HttpGet("GetUserTypes")]
        public ActionResult<List<UserType>> GetUserTypes()
        {
            return _service.GetUserTypes();
        }

        [HttpGet("GetMyProfile")]
        public UserGetRequest GetMyProfile()
        {
            return _service.GetMyProfile();
        }
    }
}