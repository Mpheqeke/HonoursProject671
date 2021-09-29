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

namespace Project.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IProjectUnitOfWork _unitOfWork;

        //private readonly IAuthenticationHelper _authentication;
        //private readonly IAuthInfo _authInfo;

        public UserService(IProjectUnitOfWork unitOfWork/*, IAuthenticationHelper authentication, IAuthInfo authInfo*/)
        {
            //_authInfo = authInfo;
            _unitOfWork = unitOfWork;
            //_authentication = authentication;
        }

        #region EEEEEEEEEEERRRRRRRRRRRRRRRRRRRROOOOOOOOOOOOOOOOOOOOOORRRRRRRRRRRRRRRRRRRRRRRR
        //Apply to A Postion
        public void ApplyToPosition(int userId, int vacId, UserJobApplication application)
        {
            try
            {
                //Users add in CVUrl and Motivation themselves

                //var vacancy = _unitOfWork.Vacancy.Query(x => x.Id == vacId).SingleOrDefault();
                var user = _unitOfWork.User.Query(x => x.Id == userId).SingleOrDefault();
                var skills = _unitOfWork.UserSkillGain.Query(x => x.UserId == userId).SingleOrDefault();
                _unitOfWork.Save();

                application.CreatedOn = DateTime.Now;
                application.ModifiedOn = DateTime.Now;

                application.VacancyId = vacId;
                application.StatusId = 1;
                application.SkillId = skills.SkillId;
                application.CreatedBy = $"{user.FirstName} {user.LastName}";
                application.ModifiedBy = $"{user.FirstName} {user.LastName}";

                application.IsActive = true;

                _unitOfWork.UserJobApplication.Add(application);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        #endregion



        //Upload CV

        //Update Profile Picture

        //FIREBASE STUFF vir UUID

        

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

        //Get Recruiters Only (Entire Model instead of DTO)
        public List<User> GetRecruiters()
        {
            var users = _unitOfWork.User.Query(x => x.IsActive).Where(x => x.RoleId == 2).ToList();
            return users;
        }

        //Get Specific User (Entire Model instead of DTO)
        public List<User> GetSingleUser(int id)
        {
            var user = _unitOfWork.User.Query(x => x.Id == id).ToList();
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
                               ImageUrl = u.ImageUrl
                           }).ToList();

            return company;
        }
        #endregion

        #region User CRUD Related Queries (Create, Update, Delete)
        //Create User
        public void CreateUser(User user)
        {
            BaseUserFactory userFactory = new AbstractUserFactory().CreateFactory(user);

            userFactory.ApplyUUID();

            user.CreatedOn = DateTime.Now;
            user.ModifiedOn = DateTime.Now;
            user.CreatedBy = $"{user.FirstName} {user.LastName}";
            user.ModifiedBy = $"{user.FirstName} {user.LastName}";
            user.IsActive = true;

            _unitOfWork.User.Add(user);
            _unitOfWork.Save();
        }

        //Update User Information (Let users update their own info)
        public void UpdateUser(int id, User user)
        {
            var updateObj = _unitOfWork.User.Query(x => x.Id == id).SingleOrDefault();

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
        public void DeleteUser(int id)
        {
            try
            {
                var delObj = _unitOfWork.User.Query(x => x.Id == id).SingleOrDefault();

                if (delObj != null)
                {
                    _unitOfWork.User.Delete(delObj);
                    _unitOfWork.Save();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }
        #endregion

        #region User Application Related Queries (NEEDS TESTING STILL)
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
            List<UserJobApplication> applications = _unitOfWork.UserJobApplication.Query(x => x.IsActive).ToList();
            List<Vacancy> vacan = _unitOfWork.Vacancy.Query(x => x.IsActive).ToList();
            List<Company> comp = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            List<User> user = _unitOfWork.User.Query(x => x.IsActive).ToList();
            List<Status> status = _unitOfWork.Status.Query().ToList();

            var applicationDetails = (from u in user
                              join a in applications on u.Id equals applicationId
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
