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

        #region Werk Nie (Need UserID in db on UserJobApplication Table)
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
        //Get All Users (With Pagination)
        public List<User> GetUsers()
        {
            var users = _unitOfWork.User.Query(x => x.IsActive).ToList();
            return users;
        }

        //Get Graduates Only (With Pagination)
        public List<User> GetGrads()
        {
            var users = _unitOfWork.User.Query(x => x.IsActive).Where(x => x.RoleId == 1).ToList();
            return users;
        }

        //Get Recruiters Only (With Pagination)
        public List<User> GetRecruiters()
        {
            var users = _unitOfWork.User.Query(x => x.IsActive).Where(x => x.RoleId == 2).ToList();
            return users;
        }

        //Get Specific User
        public List<User> GetSingleUser(int id)
        {
            var user = _unitOfWork.User.Query(x => x.Id == id).ToList();
            return user;
        }

        //View specific user info
        //View Specific Applicant Profile (With Pagination)
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

    }
}
