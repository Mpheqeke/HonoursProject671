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

        #region Moocs Select and Search
        //Retreive all Moocs
        [Route("~/api/User/GetMoocs")]
        [HttpGet]
        public ActionResult<List<MoocsDTO>> GetMoocs(int? pageNumber)
        {
            int curPage = pageNumber ?? 1;
            int curPageSize = 30;

            var users = _userService.GetMoocs();
            return Ok(users.Skip((curPage - 1) * curPageSize).Take(curPageSize));
        }

        //Search for Moocs
        [Route("~/api/User/SearchMoocs")]
        [HttpGet]
        public ActionResult<List<MoocsDTO>> SearchMoocs(int? pageNumber, string search)
        {
            int curPage = pageNumber ?? 1;
            int curPageSize = 30;

            var vacancies = _userService.SearchMoocs(search);
            return Ok(vacancies.Skip((curPage - 1) * curPageSize).Take(curPageSize));
        }
        #endregion

        #region User Select and Search Related Queries
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

        //Retreive specific user info by Id
        [Route("~/api/User/GetSpecificUser/{userId}")]
        [HttpGet("{userId}")]
        public ActionResult<List<UserDTO>> GetSpecificUser(int userId)
        {
            var user = _userService.GetSpecificUser(userId);
            return user;
        }
        #endregion

        #region User CRUD Related Queries (Create, Update, Delete)
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
        #endregion


        #region EEEEEEEEEEEEEEERRRRRRRRROOOOOOOOOOOOOOOOOOOOOOOOOOOORRRRR
        //Apply to a Postion
        [Route("~/api/User/Vacancy/ApplyToPosition/{userId}/{vacId}")]
        [HttpPost]
        public void ApplyToPosition([FromBody] UserJobApplication application, int userId, int vacId)
        {
            _userService.ApplyToPosition(userId, vacId, application);
        }
        #endregion

    }
}
