using Project.Core.AbstractFactories;
using Project.Core.DTOs;
using Project.Core.Interfaces;
using Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Core.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IProjectUnitOfWork _unitOfWork;

        public UserAuthService(IProjectUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Create User
        public int SingUpUser(User user)
        {
           BaseUserFactory userFactory = new AbstractUserFactory().CreateFactory(user);
            
            var userToSave = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mobile = user.Mobile,
                Email = user.Email,
                Gender = user.Gender,
                RoleId = user.RoleId,
                UUID = user.UUID,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                CreatedBy = $"{user.FirstName} {user.LastName}",
                ModifiedBy = $"{user.FirstName} {user.LastName}",
                IsActive = true
            };

            //userFactory.ApplyUUID();       

            _unitOfWork.User.Add(userToSave);
            _unitOfWork.Save();

            return userToSave.Id;
        }

        //Create Company
        public int SingUpCompany(Company company)
        {
            var companyToSave = new Company
            {
                Name = company.Name,
                Mission = company.Mission,
                Vacancy = company.Vacancy,
                Sector = company.Sector,
                Vision = company.Vision,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                CreatedBy = company.Name,
                ModifiedBy = company.Name,
                IsActive = true
            };   

            _unitOfWork.Company.Add(companyToSave);
            _unitOfWork.Save();

            return company.Id;
        }

        //SignIn Functionality
        public List<UserDTO> SignIn(string uuid)
        {
            List<User> user = _unitOfWork.User.Query(x => x.UUID == uuid).ToList();

            if (user != null)
            {
                var MoocList = (from u in user
                                select new UserDTO
                                {
                                    UserId = u.Id,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    Email = u.Email,
                                    Gender = u.Gender,
                                    Mobile = u.Mobile,
                                    ImageUrl = u.ImageUrl
                                }).ToList();

                return MoocList;
            }
            else
            {
                return null;
            }
        }

    }
}
