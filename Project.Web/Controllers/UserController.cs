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
        [Route("~/api/User/GetSingleUser/{id}")]
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

        //Remove a user (FK CONSTRAINT ERROR)
                //The DELETE statement conflicted with the REFERENCE constraint "FK_CompanyRepresentative_User". 
                //The conflict occurred in database "ITRI671Project", table "dbo.CompanyRepresentative", column 'UserId'.
                //The statement has been terminated.
        [Route("~/api/User/DeleteUser/{id}")]
        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
            _userService.DeleteUser(id);
        }

        //Update existing user
        [Route("~/api/User/UpdateUser/{id}")]
        [HttpPut("{id}")]
        public void UpdateUser(int id, [FromBody] User user)
        {
            _userService.UpdateUser(id, user);
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

        #region convert image to byte
        //[Route("~/api/User/UploadImage/{userId}")]
        //[HttpPost("{userId}")]
        //public void UploadImage([FromForm] IFormFile file, int userId)
        //{
        //    if (file.Length > 0)
        //    {
        //        string filePath = System.IO.Path.Combine(_hostEnvironment.ContentRootPath, "Images", file.FileName);

        //        using (var stream = System.IO.File.Create(filePath))
        //        {
        //            file.CopyTo(stream);
        //        }

        //        _userService.UploadImage(file, filePath, userId);
        //    }

        //    var fileUrl = $"{this.Request.Scheme}://{this.Request.Host}/images/{file.FileName}";
        //    //return fileUrl;

        //    //var imagePath = Path.Combine(img.ToString(), img.FileName);
        //    //_userService.UploadImage(img, imagePath);
        //}
        //https://www.c-sharpcorner.com/UploadFile/1a81c5/convert-file-to-byte-array-and-byte-array-to-files/
        #endregion

        #region User Image and File Upload, View, and Download Related Queries
        //Allow user to upload/update profile image
        [Route("~/api/User/UploadImage/{userId}")]
        [HttpPost("{userId}")]
        public void UploadImage([FromForm] IFormFile file, int userId)
        {
            if (file.Length > 0)
            {
                string filePath = System.IO.Path.Combine(_hostEnvironment.ContentRootPath, "Images", file.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                //string fileUrl = $"{this.Request.Scheme}://{this.Request.Host}/images/{file.FileName}";
                _userService.UploadImage(filePath, userId);       
            }
        }

        //Get profile picture of specific user
        [Route("~/api/User/GetUserProfilePicture/{userId}")]
        [HttpGet("{userId}")]
        public ActionResult GetUserProfilePicture(int userId, string path)
        {
            path = _userService.GetImagePath(path, userId);
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return File(_userService.GetUserProfilePicture(userId), GetMimeTypes()[ext]);        
        }

        //Allow user to upload/update CV
        [Route("~/api/User/UploadCV/{userId}")]
        [HttpPost("{userId}")]
        public void UploadFile([FromForm] IFormFile file, int userId)
        {
            if (file.Length > 0)
            {
                UserDocument document = new UserDocument();
                string filePath = System.IO.Path.Combine(_hostEnvironment.ContentRootPath, "CVs", file.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                //string fileUrl = $"{this.Request.Scheme}://{this.Request.Host}/images/{file.FileName}";
                _userService.UploadCV(filePath, userId, document);
            }
        }

        //View CV of specific user
        [Route("~/api/User/GetUserCV/{userId}")]
        [HttpGet("{userId}")]
        public ActionResult GetUserCV(int userId, string path)
        {
            path = _userService.GetFilePath(path, userId);
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return File(_userService.GetUserDocument(userId), GetMimeTypes()[ext]);
        }

        //Download CV of specific user
        [Route("~/api/User/DownloadUserCV/{userId}")]
        [HttpGet("{userId}")]
        public ActionResult DownloadUserCV(int userId, string path)
        {
            path = _userService.GetFilePath(path, userId);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(path));
        }

        //Allow user to upload Course Certificate
        [Route("~/api/User/UploadCourseCert/{userId}")]
        [HttpPost("{userId}")]
        public void UploadCourseCert([FromForm] IFormFile file, int userId)
        {
            if (file.Length > 0)
            {
                UserDocument document = new UserDocument();
                string filePath = System.IO.Path.Combine(_hostEnvironment.ContentRootPath, "CVs", file.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                //string fileUrl = $"{this.Request.Scheme}://{this.Request.Host}/images/{file.FileName}";
                _userService.UploadCourseCert(filePath, userId, document);
            }
        }

        //Get all of the course ceritifcates of a user
        [Route("~/api/User/Test/{userId}")]
        [HttpGet("{userId}")]
        public ActionResult<List<CourseCertDTOcs>> GetUserCourseCertificates(int userId)
        {
            return _userService.GetUserCourseCertificates(userId);
        }

        //View single Course Certificate of specific user
        [Route("~/api/User/GetUserCourseCert/{userId}/{docId}")]
        [HttpGet("{docId}")]
        public ActionResult GetUserCourseCert(int userId, int docId, string path)
        {
            path = _userService.GetCourseCertPath(path, userId, docId);
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return File(_userService.GetUserCourseCert(userId, docId), GetMimeTypes()[ext]);
        }

        //Download Course Certificate of specific user
        [Route("~/api/User/DownloadUserCourseCert/{userId}/{docId}")]
        [HttpGet("{docId}")]
        public ActionResult DownloadUserCourseCert(int userId, string path, int docId)
        {
            path = _userService.GetCourseCertPath(path, userId, docId);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(path));
        }

        //Dictionary defining the different types of documents and imnages
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
            };
        }
        #endregion
    }
}
