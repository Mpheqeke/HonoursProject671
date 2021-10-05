using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTOs;
using Project.Core.Interfaces;
using Project.Core.Models;

namespace Project.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    [Authorize]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;

        public UserAuthController(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        //Create new user
        [Route("~/api/Auth/SingUpUser")]
        [HttpPost]
        public int SingUpUser([FromBody] User user)
        {
            return _userAuthService.SingUpUser(user);
        }

        //Create new company
        [Route("~/api/Auth/SingUpCompany")]
        [HttpPost]
        public int SingUpCompany([FromBody] Company company)
        {
            return _userAuthService.SingUpCompany(company);
        }

        //Allow users to sign in
        [HttpPost]
        [AllowAnonymous]
        [Route("~/api/Auth/SingIn")]
        public List<UserDTO> SingIn(string uuid)
        {
            return _userAuthService.SignIn(uuid);
        }
    }
}
