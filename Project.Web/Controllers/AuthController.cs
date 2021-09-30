using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTOs;
using Project.Core.Interfaces;

namespace Project.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IFireBaseAuth _fireBaseAuth;

        public AuthController(IFireBaseAuth fireBaseAuth)
        {
            _fireBaseAuth = fireBaseAuth;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("~/api/Auth/SingUp")]
        public SignUpDTO SingUp(FireBaseAuthDTO fireBaseAuthDTO)
        {
            return _fireBaseAuth.SingUp(fireBaseAuthDTO);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("~/api/Auth/SingIn")]
        public SignUpDTO SingIn(FireBaseAuthDTO fireBaseAuthDTO)
        {
            return _fireBaseAuth.SingIn(fireBaseAuthDTO);
        }
    }
}
