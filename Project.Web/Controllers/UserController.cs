using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTOs;
using Project.Core.Global;
using Project.Core.Interfaces;
using Project.Core.Models;
using Project.Web.Filters;
using System.Collections.Generic;

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
        public ActionResult<List<User>> GetUsers()
        {
            var users = _userService.GetUsers();
            return users;
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

        [Route("~/api/User/temp")]
        [HttpGet]
        public ActionResult<List<User>> temp()
        {
            var temp = _userService.temp();
            return temp;
        }

    }
}
