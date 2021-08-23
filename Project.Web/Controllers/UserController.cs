using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTOs;
using Project.Core.Global;
using Project.Core.Interfaces;
using Project.Core.Models;
using Project.Web.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Project.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Retreive all users
        [Route("~/api/User/GetUsers")]
        [HttpGet]
        public ActionResult<List<User>> GetUsers(int? pageNumber)
        {
            int curPage = pageNumber ?? 1;
            int curPageSize = 30;

            var users = _userService.GetUsers();
            return Ok(users.Skip((curPage - 1) * curPageSize).Take(curPageSize));
        }

        //Retreive specific users
        [Route("~/api/User/GetGrads")]
        [HttpGet]
        public ActionResult<List<User>> GetGrads(int? pageNumber)
        {
            int curPage = pageNumber ?? 1;
            int curPageSize = 30;

            var users = _userService.GetGrads();
            return Ok(users.Skip((curPage - 1) * curPageSize).Take(curPageSize));
        }

        [Route("~/api/User/GetRecruiters")]
        [HttpGet]
        public ActionResult<List<User>> GetRecruiters(int? pageNumber)
        {
            int curPage = pageNumber ?? 1;
            int curPageSize = 30;

            var users = _userService.GetRecruiters();
            return Ok(users.Skip((curPage - 1) * curPageSize).Take(curPageSize));
        }

        //Retreive user by Id
        [Route("~/api/User/{id}")]
        [HttpGet("{id}")]
        public ActionResult<List<User>> GetSingleUser(int id)
        {
            var user = _userService.GetSingleUser(id);
            return user;
        }

        //Create new user
        [Route("~/api/User/CreateUser")]
        [HttpPost]
        public void CreateUser([FromBody] User user)
        {
            _userService.CreateUser(user);
        }

        //Remove a user
        [Route("~/api/User/{id}")]
        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
            _userService.DeleteUser(id);
        }

        //Update existing user
        [Route("~/api/User/{id}")]
        [HttpPut("{id}")]
        public void UpdateUser(int id, [FromBody] User user)
        {
            _userService.UpdateUser(id, user);
        }


    }
}
