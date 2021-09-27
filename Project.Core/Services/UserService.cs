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

        //Upload CV

        //Update Profile Picture

        //FIREBASE STUFF vir UUID

    }
}
