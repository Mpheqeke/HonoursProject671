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


namespace Project.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IProjectUnitOfWork _unitOfWork;
        private readonly IAuthenticationHelper _authentication;
        private readonly IAuthInfo _authInfo;
      

        public UserService(IProjectUnitOfWork unitOfWork, IAuthenticationHelper authentication, IAuthInfo authInfo)
        {
            _authInfo = authInfo;
            _unitOfWork = unitOfWork;
            _authentication = authentication;
        }


    }
}
