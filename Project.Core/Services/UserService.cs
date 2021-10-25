using Project.Core.DTOs;
using Project.Core.Global;
using Project.Core.Interfaces;
using Project.Core.Models;
using Project.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.Core.AbstractFactories;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Project.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IProjectUnitOfWork _unitOfWork;
        private readonly IGoogleCloudStorage _googleCloudStorage;

        //private readonly IAuthenticationHelper _authentication;
        //private readonly IAuthInfo _authInfo;

        public UserService(IProjectUnitOfWork unitOfWork, IGoogleCloudStorage googleCloudStorage/*, IAuthenticationHelper authentication, IAuthInfo authInfo*/)
        {
            //_authInfo = authInfo;
            _unitOfWork = unitOfWork;
            _googleCloudStorage = googleCloudStorage;
            //_authentication = authentication;
        }

        #region Profile Image Functionalities

        //Get the name of the file being uploaded
        public string FormFileName(string userName, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{userName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
            return fileNameForStorage;
        }

        //Upload the actual file / replace existin one /((WERK))
        public async Task UploadProfileImage(IFormFile file, int userId)
        {
            try
            {
                var updateObj = _unitOfWork.User.Query(x => x.Id == userId).FirstOrDefault();

                if (updateObj != null)
                {
                    if (updateObj.ImageUrl != null)
                    {
                        await _googleCloudStorage.DeleteFileAsync(updateObj.ImageName);
                    }

                    string fileNameForStorage = FormFileName(updateObj.DisplayName, file.FileName);
                    updateObj.ImageUrl = await _googleCloudStorage.UploadFileAsync(file, fileNameForStorage);
                    updateObj.ImageName = fileNameForStorage;

                    _unitOfWork.User.Update(updateObj);
                    _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
           
        }

        //Retreives the user profile picture
        public string GetUserProfilePicture(int userId)
        {
            var userToQuery = _unitOfWork.User.Query(x => x.Id == userId).FirstOrDefault();
            var url = _googleCloudStorage.GetFileAsync(userToQuery.ImageName);

            return url;
        }
        #endregion

        #region User Documents Functionalities
        public string CourseCertFileName(string userName, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{userName}-{Path.GetFileNameWithoutExtension(fileName)}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
            return fileNameForStorage;
        }

        //Allow users to upload CV documents /((WERK))
        public async Task UploadCVDocument(IFormFile file, int userId)
        {
            try
            {
                var userObj = _unitOfWork.User.Query(x => x.Id == userId).FirstOrDefault();
                var docObj = _unitOfWork.UserDocument.Query(x => x.UserId == userId).FirstOrDefault();

                if (userObj != null)
                {
                    if (docObj != null)
                    {
                        await _googleCloudStorage.DeleteFileAsync(docObj.DocumentName);
                        _unitOfWork.UserDocument.Delete(docObj);
                        _unitOfWork.Save();
                    }

                    string fileNameForStorage = FormFileName(userObj.DisplayName + " CV", file.FileName);
                    var docToAdd = new UserDocument
                    {
                        UserId = userId,
                        DocumentTypeId = 1,
                        StatusId = 1,
                        DocumentUrl = await _googleCloudStorage.UploadFileAsync(file, fileNameForStorage),
                        DocumentName = fileNameForStorage
                    };

                    _unitOfWork.UserDocument.Add(docToAdd);
                    _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
           
        }

        //Allow users to upload course certificates /((WERK))
        public async Task UploadCourseCert(IFormFile file, int userId)
        {
            try
            {
                var userObj = _unitOfWork.User.Query(x => x.Id == userId).FirstOrDefault();
                if (userObj != null)
                {
                    string fileNameForStorage = CourseCertFileName(userObj.DisplayName + " Course Certificate", file.FileName);
                    var docToAdd = new UserDocument
                    {
                        UserId = userId,
                        DocumentTypeId = 3,
                        StatusId = 1,
                        DocumentUrl = await _googleCloudStorage.UploadFileAsync(file, fileNameForStorage),
                        DocumentName = fileNameForStorage
                    };

                    _unitOfWork.UserDocument.Add(docToAdd);
                    _unitOfWork.Save();
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }          
        }

        //Allow users to delete a course certificate /((WERK))
        public async Task DeleteCourseCert(int docId)
        {
            try
            {
                var delObj = _unitOfWork.UserDocument.Query(x => x.Id == docId).FirstOrDefault();

                if (delObj != null)
                {
                    await _googleCloudStorage.DeleteFileAsync(delObj.DocumentName);
                    _unitOfWork.UserDocument.Delete(delObj);
                    _unitOfWork.Save();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        //Get list of all User Course Certificates
        public List<CourseCertDTOcs> GetUserCourseCertificates(int userId)
        {
            List<User> user = _unitOfWork.User.Query(x => x.Id == userId).ToList();
            List<UserDocument> doc = _unitOfWork.UserDocument.Query(x => x.DocumentTypeId == 3).ToList();

            var company = (from u in user
                           join ud in doc on u.Id equals ud.UserId
                           where u.Id == userId
                           select new CourseCertDTOcs
                           {
                               DocumentUrl = ud.DocumentUrl,
                               DocumentName = Path.GetFileNameWithoutExtension(ud.DocumentUrl),
                               CourseCertId = ud.Id
                           }).ToList();

            return company;
        }

        //Retreives any user document based on docId
        public string GetUserDocument(int docId)
        {
            var docToQuery = _unitOfWork.UserDocument.Query(x => x.Id == docId).FirstOrDefault();
            var url = _googleCloudStorage.GetFileAsync(docToQuery.DocumentName);

            return url;
        }
        #endregion


        #region Moocs Select and Search
        //Get all Moocs
        public List<MoocsDTO> GetMoocs()
        {
            List<Moocs> moocs = _unitOfWork.Moocs.Query().ToList();

            var MoocList = (from m in moocs
                            select new MoocsDTO
                            {
                                MoocName = m.Name,
                                MoocUrl = m.Url
                            }).ToList();

            return MoocList;
        }

        //Search for Moocs
        public List<MoocsDTO> SearchMoocs(string search)
        {
            List<Moocs> moocs = _unitOfWork.Moocs.Query().ToList();

            var MoocList = (from m in moocs
                            where m.Name.ToUpper().Contains(search.ToUpper())
                               || m.Url.ToUpper().Contains(search.ToUpper())
                            select new MoocsDTO
                            {
                                MoocName = m.Name,
                                MoocUrl = m.Url
                            }).ToList();

            return MoocList;
        }
        #endregion

        #region User Select and Search Related Queries
        //Get All Users (Entire Model instead of DTO)
        public List<User> GetUsers()
        {
            var users = _unitOfWork.User.Query(x => x.IsActive).ToList();
            return users;
        }

        //Get Graduates Only (Entire Model instead of DTO)
        public List<User> GetGrads()
        {
            var users = _unitOfWork.User.Query(x => x.IsActive).Where(x => x.RoleId == 1).ToList();
            return users;
        }


        //Get Specific User (Entire Model instead of DTO)
        public List<User> GetSingleUser(int userId)
        {
            var user = _unitOfWork.User.Query(x => x.Id == userId).ToList();
            return user;
        }

        //View specific user info
        public List<UserDTO> GetSpecificUser(int userId)
        {
            List<User> user = _unitOfWork.User.Query(x => x.IsActive).ToList();

            var company = (from u in user
                           where u.Id == userId
                           select new UserDTO
                           {
                               UserId = userId,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               Gender = u.Gender,
                               Email = u.Email,
                               Mobile = u.Mobile,
                               ImageUrl = u.ImageUrl,
                               RoleId = u.RoleId
                           }).ToList();

            return company;
        }
        #endregion

        #region User CRUD Related Queries (Create, Update, Delete)
        
        //Update User Information (Let users update their own info)
        public void UpdateUser(int userId, User user)
        {
            var updateObj = _unitOfWork.User.Query(x => x.Id == userId).FirstOrDefault();

            if (updateObj != null)
            {
                updateObj.FirstName = user.FirstName;
                updateObj.LastName = user.LastName;
                updateObj.Mobile = user.Mobile;
                updateObj.Gender = user.Gender;
                updateObj.Email = user.Email;

                updateObj.ModifiedBy = $"{user.FirstName} {user.LastName}";
                updateObj.ModifiedOn = DateTime.Now;

                _unitOfWork.User.Update(updateObj);
                _unitOfWork.Save();
            }
        }

        //Delete User 
        public void DeleteUser(int userId)
        {
            try
            {
                //isactive = false
                var delObj = _unitOfWork.User.Query(x => x.Id == userId).FirstOrDefault();

                if (delObj != null)
                {
                    delObj.IsActive = false;
                    _unitOfWork.User.Update(delObj);
                    _unitOfWork.Save();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }
        #endregion

        #region User Application Related Queries

        //Allow users to apply to vacancies
        public void ApplyToPosition(int userId, int vacId, UserJobApplication application)
        {
            try
            {
                //Users add in CVUrl and Motivation themselves

                var user = _unitOfWork.User.Query(x => x.Id == userId).SingleOrDefault();
                var skills = _unitOfWork.UserSkillGain.Query(x => x.UserId == userId).SingleOrDefault();
                
                var applicationToAdd = new UserJobApplication
                {
                    UserId = user.Id,
                    VacancyId = vacId,
                    StatusId = 1,
                    SkillId = skills !=null ? skills.SkillId: application.SkillId,
                    Motivation = application.Motivation,
                    CVUrl = application.CVUrl,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    CreatedBy = $"{user.FirstName} {user.LastName}",
                    ModifiedBy = $"{user.FirstName} {user.LastName}",

                };
                _unitOfWork.UserJobApplication.Add(applicationToAdd);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        //Show All Applications for Specific User (Can click on JobTitle and will show all details for that application)
        public List<UserApplicationsDTO> GetApplications(int userId)
        {
            List<UserJobApplication> applications = _unitOfWork.UserJobApplication.Query(x => x.IsActive).ToList();
            List<Vacancy> vacan = _unitOfWork.Vacancy.Query(x => x.IsActive).ToList();
            List<Company> comp = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            List<User> user = _unitOfWork.User.Query(x => x.IsActive).ToList();
            List<Status> status = _unitOfWork.Status.Query().ToList();

            var applicationDetails = (from u in user
                                      join a in applications on u.Id equals a.UserId
                                      join v in vacan on a.VacancyId equals v.Id
                                      join s in status on a.StatusId equals s.Id
                                      where u.Id == userId
                                      select new UserApplicationsDTO
                                      {
                                          ApplicationId = a.Id,
                                          JobTitle = v.JobTitle,
                                          ApplicationStatus = s.Name
                                      }).ToList();

            return applicationDetails;
        }

        //Allow User to View Own specific Applications (Can click company Name from here to view company)
        public List<UserAppliDetailsDTO> ViewApplication(int userId, int applicationId)
        {
            List<UserJobApplication> applications = _unitOfWork.UserJobApplication.Query().ToList();
            List<Vacancy> vacan = _unitOfWork.Vacancy.Query(x => x.IsActive).ToList();
            List<Company> comp = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            List<User> user = _unitOfWork.User.Query(x => x.IsActive).ToList();
            List<Status> status = _unitOfWork.Status.Query().ToList();

            var applicationDetails = (from u in user
                              join a in applications on u.Id equals userId
                              join v in vacan on a.VacancyId equals v.Id
                              join s in status on a.StatusId equals s.Id
                              join c in comp on v.CompanyId equals c.Id
                              where u.Id == userId && a.Id == applicationId
                              select new UserAppliDetailsDTO
                              {
                                  CompId = c.Id,
                                  CompanyName = c.Name,
                                  JobTitle = v.JobTitle,
                                  JobDescription = v.JobDescription,
                                  Location = v.Location,
                                  Responsibilities = v.Responsibilities,
                                  StartDate = v.StartDate,
                                  ApplicationStatus = s.Name
                              }).ToList();

            return applicationDetails;
        }

        //Allow User to Delete Own Applicantions
        public void DeleteApplication(int applicationId)
        {
            try
            {
                var delObj = _unitOfWork.UserJobApplication.Query(x => x.Id == applicationId).SingleOrDefault();

                if (delObj != null)
                {
                    _unitOfWork.UserJobApplication.Delete(delObj);
                    _unitOfWork.Save();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        #endregion

    }
}
