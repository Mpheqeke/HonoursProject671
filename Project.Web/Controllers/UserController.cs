using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTOs;
using Project.Core.Global;
using Project.Core.Interfaces;
using Project.Core.Models;
using Project.Web.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Project.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _hostEnvironment;

        public UserController(IUserService userService, IHostingEnvironment hostEnvironment)
        {
            _userService = userService;
            _hostEnvironment = hostEnvironment;
        }

        #region Profile Picture Queries
        //Upload Profile Image / Replace Exisiting One
        [Route("~/api/User/UploadProfileImage/{userId}")]
        [HttpPut("{userId}")]
        public async Task UploadProfileImage(int userId, [FromForm] IFormFile file)
        {
            try
            {
                await _userService.UploadProfileImage(file, userId);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        //Get profile picture of specific user
        [Route("~/api/User/GetUserProfilePicture/{userId}")]
        [HttpGet("{userId}")]
        public void GetUserProfilePicture(int userId)
        {
            var url = _userService.GetUserProfilePicture(userId);
            Response.Redirect(url);
        }
        #endregion

        #region User Document Queries
        //Upload User CV / Replace Exisiting One
        [Route("~/api/User/UploadCVDocument/{userId}")]
        [HttpPost("{userId}")]
        public async Task UploadCVDocument(int userId, [FromForm] IFormFile file)
        {
            try
            {
                await _userService.UploadCVDocument(file, userId);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        //Upload User Course Certificate / Replace Exisiting One
        [Route("~/api/User/UploadCourseCert/{userId}")]
        [HttpPost("{userId}")]
        public async Task UploadCourseCert(int userId, [FromForm] IFormFile file)
        {
            try
            {
                await _userService.UploadCourseCert(file, userId);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        //Remove course certificate
        [Route("~/api/User/DeleteCourseCert/{docId}")]
        [HttpDelete("{docId}")]
        public async Task DeleteCourseCert(int docId)
        {
            try
            {
                await _userService.DeleteCourseCert(docId);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        //Get document of specific user
        [Route("~/api/User/GetUserDocument/{docId}")]
        [HttpGet("{userId}")]
        public void GetUserDocument(int docId)
        {
            var url = _userService.GetUserDocument(docId);
            Response.Redirect(url);
        }

        //Get LIST containing all of the course ceritifcates of a user
        [Route("~/api/User/GetUserCourseCertificates/{userId}")]
        [HttpGet("{userId}")]
        public ActionResult<List<CourseCertDTOcs>> GetUserCourseCertificates(int userId)
        {
            return _userService.GetUserCourseCertificates(userId);
        }

        #endregion


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


        //Retreive user by Id
        [Route("~/api/User/GetSingleUser/{userId}")]
        [HttpGet("{userId}")]
        public ActionResult<List<User>> GetSingleUser(int userId)
        {
            var user = _userService.GetSingleUser(userId);
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

        //Remove a user
        [Route("~/api/User/DeleteUser/{userId}")]
        [HttpPut("{userId}")]
        public void DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
        }

        //Update existing user
        [Route("~/api/User/UpdateUser/{userId}")]
        [HttpPut("{userId}")]
        public void UpdateUser(int userId, [FromBody] User user)
        {
            _userService.UpdateUser(userId, user);
        }
        #endregion

        #region User Application Related Queries
        //Apply to a Postion
        [Route("~/api/User/Vacancy/ApplyToPosition/{userId}/{vacId}")]
        [HttpPost]
        public void ApplyToPosition([FromBody] UserJobApplication application, int userId, int vacId)
        {
            _userService.ApplyToPosition(userId, vacId, application);
        }

        //Get all application for specific user
        [Route("~/api/User/GetApplications/{userId}")]
        [HttpGet("{userId}")]
        public ActionResult<List<UserApplicationsDTO>> GetApplications(int userId)
        {
            return _userService.GetApplications(userId);
        }

        //Get specific application details for specific user
        [Route("~/api/User/ViewApplication/{userId}/{applicationId}")]
        [HttpGet("{applicationId}")]
        public ActionResult<List<UserAppliDetailsDTO>> ViewApplication(int userId, int applicationId)
        {
            return _userService.ViewApplication(userId, applicationId);
        }

        //Allow user to remove specific application
        [Route("~/api/User/DeleteApplication/{applicationId}")]
        [HttpDelete("{applicationId}")]
        public void DeleteApplication(int applicationId)
        {
            _userService.DeleteApplication(applicationId);
        }
        #endregion

    }
}
